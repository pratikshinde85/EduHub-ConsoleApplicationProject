namespace EduHub_Repository_Console_Project{

    class Feedbacks
    {
        public int FeedbackId{ get; set; }
        public int UserId{ get; set; }  
        public int CourseId{ get; set; }

        public string? Feedback{get;set; }

        public string?Date{ get; set; }

    }
}