namespace src.instances
{
    public static class DataBlockExtension
    {
        public static void FillBlocks(this DataBlock[] obj, int initSize)
        {
            for (int i = 0; i < initSize; i++)
                obj[i] = new DataBlock();
        }
    }
}