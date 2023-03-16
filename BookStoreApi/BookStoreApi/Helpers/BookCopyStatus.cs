namespace BookStoreApi.Helpers
{
	public static class BookCopyStatus
	{
		public static readonly String Sold = "Sold";
		public static readonly String Borrowed = "Borrowed";
		public static readonly String Availble = "Availble";

		public static bool CheckBookCopyStatus(String Status)
		{
			if (Status.Equals(Sold) || Status.Equals(Borrowed) || Status.Equals(Availble))
			{
				return true;
			}
			return false;
		}
	}
}
