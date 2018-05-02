//using System;
//using System.Collections.Generic;
//using System.Text;
//using VirtualClassroom.Domain;

//namespace VirtualClassroom.Persistence.Memory
//{
//    public class MemStudentsRepository : IStudentRepository
//    {
//        List<Student> students = new List<Student>();

//        public void Add(Student student)
//        {
//            students.Add(student);
//        }

//        public void Delete(Student student)
//        {
//            students.Remove(student);
//        }

//        public List<Activity> GetActivities(int studentIdentifier)
//        {
//            var student = students.Find(stud => stud.Id == studentIdentifier);

//            return new List<Activity>(student.Activities);
//        }

//        public IEnumerable<Student> GetAll()
//        {
//            return students;
//        }

//        public Student GetByEmail(string email)
//        {
//            return students.Find(stud => stud.Email == email);
//        }

//        public Student GetById(int identifier)
//        {
//            return students.Find(stud => stud.Id == identifier);
//        }

//        public Student GetByName(string name)
//        {
//            return students.Find(stud =>
//                $"{stud.FirstName} {stud.LastName}" == name ||
//                $"{stud.LastName} {stud.FirstName}" == name
//            );
//        }

//        public void Save()
//        {

//        }
//    }
//}
