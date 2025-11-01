namespace MinimalApiApp
{
    class Handlers
    {
        // instance method
        public void ReplaceFruit(string id, Fruit fruit)
        {
            Fruit.All[id] = fruit;
        }

        // convert response to JSON object
        public static void AddFruit(string id, Fruit fruit)
        {
            Fruit.All.Add(id, fruit);
        }
    }
}
