namespace Assignment4_2.Models
{
    public class Pagination
    {
        private int NrOfPages = 100;
        public int PageSize
        {
            get
            {
                return NrOfPages;
            }
            set
            {
                if (value > 100)
                    NrOfPages = 100;
                else
                    NrOfPages = value;
            }
        }
    }
}
