using AutoMapper;
using BookStoreApi.Data;
using BookStoreApi.Models;

namespace BookStoreApi.Helpers
{
	public class ApplicationMapper : Profile
	{
		public ApplicationMapper() 
		{ 
			CreateMap<Book, BookModel>().ReverseMap();
			CreateMap<Author, AuthorModel>().ReverseMap();
			CreateMap<BookCopy, BookCopyModel>().ReverseMap();
			CreateMap<Category, CategoryModel>().ReverseMap();
			CreateMap<EBook, EBookModel>().ReverseMap();
			CreateMap<Language, LanguageModel>().ReverseMap();
			CreateMap<Publisher, PublisherModel>().ReverseMap();
			CreateMap<Section, SectionModel>().ReverseMap();
		}
	}
}
