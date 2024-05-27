namespace ZATCAMAUI.Models.SubmitReportModel

{
    public class CategoryDataResponse
    {
        public string id { get; set; }
        public string title { get; set; }
        public string related { get; set; }
        public string message { get; set; }
        public string Id { get { return id; } }
        public string Title { get { return title; } }
        public string Related { get { return related; } }
        public string Message { get { return message; } }

    }
    public class CategoryResponseModel
    {
        public List<CategoryDataResponse> categories { get; set; }
    }
    public class SubCategoryResponseModel
    {
        public List<CategoryDataResponse> subCategoryList { get; set; }
    }
}
