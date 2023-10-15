public static class StoryFiller
{
    public static StoryNode FillStory()
    {
        var root = CreateNode(
            "Te encuentras en una habitación y no recuerdas nada. Quieres salir",
            new [] {
            "Explorar objetos",
            "Explorar habitación"});

        var node1 = CreateNode(
            "Hay una silla y una mesa con una planta a la izquierda. " +
            "A la derecha hay una estantería con libros. " +
            "Detrás parece que hay unas cajas.",
            new [] {
            "Ir a la derecha",
            "Ir a la izquierda",
            "Ir hacia atrás",
            "Explorar habitación"});

        var node2 = CreateNode(
            "Nada interesante... " +
            "Aunque hay un libro que llama la atención... " +
            "¿Botánica para astronautas?",
            new [] {
            "Explorar el resto de la habitación",
            "Averiguar más del libro raro."});

        var node3 = CreateNode(
           "Parece que habla de plantas, qué sorpresa. " +
           "Hay una señalada, se llama plantus corrientis.",
           new [] {
            "Dejar el libro en su sitio y explorar el resto de objetos de la habitación."});

        var node4 = CreateNode(
           "Nada interesante en las cajas, están llenas de libros... " +
           "Deberían estar en la estantería.",
           new [] {
            "Volver y explorar el resto de objetos de la habitación."});

        var node5 = CreateNode(
           "Humm, una silla. Te duele la cabeza, así que te sientas.",
           new [] {
            "Quiero ver lo de la mesa también."});

        var node6 = CreateNode(
            "La mesa en sí no tiene nada de especial, tiene un poco de tierra de la planta. " +
            "Los cajones de la mesa parecen entreabiertos.",
            new [] {
            "Explorar los cajones.",
            "Volver a explorar los otros objetos"});

        var node6Bis = CreateNode(
           "La mesa en sí no tiene nada de especial, tiene un poco de tierra de la planta. " +
           "La etiqueta de la planta pone plantus corrientis. " +
           "Los cajones de la mesa parecen entreabiertos.",
           new [] {
            "Explorar los cajones.",
            "Mirar la planta de cerca.",
            "Volver a explorar los otros objetos"});

        var node7 = CreateNode(
          "Los cajones están vacíos, mejor explorar otra cosa.",
          new [] {
            "Volver a explorar los otros objetos"});

        var node8 = CreateNode(
           "¡¡Al levantar la planta encuentras una llave!! ¿Qué abrirá?",
           new [] {
            "Explorar la habitación"});

        var node9 = CreateNode(
          "La habitación tiene un par de ventanas y una puerta.",
          new [] {
            "Ira la ventana #1",
            "Ira la ventana #2",
            "Ira la puerta"});

        var node10 = CreateNode(
          "La ventana está tapiada, no se puede abrir.",
          new [] {
            "Ira la otra ventana",
            "Ira la puerta"});

        var node11 = CreateNode(
          "La puerta está cerrada con un candado.",
          new [] {
            "Explorar los objetos de la habitación"});

        var node11Bis = CreateNode(
          "La puerta está cerrada con un candado.",
          new [] {
            "Explorar los objetos de la habitación",
            "Usar la llave"});

        var node12 = CreateNode(
          "¡¡Has salido de la habitación!!",
          new [] {
            "Salir del juego"});

        root.NextNode[0] = node1;
        root.NextNode[1] = node9;

        node1.NextNode[0] = node2;
        node1.NextNode[1] = node5;
        node1.NextNode[2] = node4;
        node1.NextNode[3] = node9;

        node2.NextNode[0] = node1;
        node2.NextNode[1] = node3;

        node3.NextNode[0] = node1;
        node3.OnNodeVisited = () =>
        {
            node5.NextNode[0] = node6Bis;
        };

        node4.NextNode[0] = node1;

        node5.NextNode[0] = node6;

        node6.NextNode[0] = node7;
        node6.NextNode[1] = node1;

        node6Bis.NextNode[0] = node7; 
        node6Bis.NextNode[1] = node8; 
        node6Bis.NextNode[2] = node1;
        
        node7.NextNode[0] = node1;

        node8.NextNode[0] = node9;
        node8.OnNodeVisited = () =>
        {
            node9.NextNode[2] = node10.NextNode[1] = node11Bis;
        };

        node9.NextNode[0] = node9.NextNode[1] = node10;
        node9.NextNode[2] = node11;

        node10.NextNode[0] = node10;
        node10.NextNode[1] = node11;

        node11.NextNode[0] = node1;

        node11Bis.NextNode[0] = node1;
        node11Bis.NextNode[1] = node12;

        node12.IsFinal = true;

        return root;
    }

    private static StoryNode CreateNode(string history, string[] options)
    {
        var node = new StoryNode
        {
            History = history,
            Answers = options,
            NextNode = new StoryNode[options.Length]
        };
        return node;
    }
}
