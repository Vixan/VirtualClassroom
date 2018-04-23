namespace VirtualClassroom.Persistence.EF
{
    class PersistanceContext : IPersistanceContext
    {
        private ActivitiesRepository activitiesRepository = new ActivitiesRepository();
        private ProfessorRepository professorRepository = new ProfessorRepository();
        private StudentRepository studentRepository = new StudentRepository();

        public IActivitiesRepository GetActivitiesRepository()
        {
            return activitiesRepository;
        }

        public IProfessorRepository GetProfessorRepository()
        {
            return professorRepository;
        }

        public IStudentRepository GetStudentRepository()
        {
            return studentRepository;
        }
    }
}
