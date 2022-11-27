namespace EducationSearchV3.Extensions
{
    public static class ICollectionExtension
    {
        public static void Replace<T>(this ICollection<T> baseC, ICollection<T> incommingC)
        {
            if(baseC is null)
                baseC = new List<T>(incommingC.Count);
            if(baseC.Any())
                baseC.Clear();
            foreach (var item in incommingC)
            {
                baseC!.Add(item);
            }            
        }
    }
}
