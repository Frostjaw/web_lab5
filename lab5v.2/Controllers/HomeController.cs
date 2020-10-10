using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab5v._2.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using lab5v2.Models;

namespace lab5v._2.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hospitals()
        {
            List<Hospital> hospitals = new List<Hospital>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Hospital";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Hospital hospital = new Hospital();
                        hospital.Id = Convert.ToInt32(dataReader["Id"]);
                        hospital.Name = Convert.ToString(dataReader["Name"]);
                        hospital.Address = Convert.ToString(dataReader["Address"]);
                        hospitals.Add(hospital);
                    }
                }
                connection.Close();
            }
            return View(hospitals);
        }

        public IActionResult CreateHospital(string name, string address)
        {
            if (HttpContext.Request.Method == "POST")
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Hospital (Name, Address) Values ('{name}','{address}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Hospitals");
                }
            }

            return View();
        }

        public IActionResult UpdateHospital(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            Hospital hospital = new Hospital();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Hospital Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        hospital.Id = Convert.ToInt32(dataReader["Id"]);
                        hospital.Name = Convert.ToString(dataReader["Name"]);
                        hospital.Address = Convert.ToString(dataReader["Address"]);
                    }
                }
                connection.Close();
            }
            return View(hospital);
        }

        [HttpPost]
        [ActionName("UpdateHospital")]
        public IActionResult Update_Post(Hospital hospital)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Hospital SET Name='{hospital.Name}', Address='{hospital.Address}' Where Id='{hospital.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Hospitals");
        }

        [HttpPost]
        public IActionResult DeleteHospital(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Hospital Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }
            return RedirectToAction("Hospitals");
        }

        public IActionResult DetailsHospital(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            Hospital hospital = new Hospital();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Hospital Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        hospital.Id = Convert.ToInt32(dataReader["Id"]);
                        hospital.Name = Convert.ToString(dataReader["Name"]);
                        hospital.Address = Convert.ToString(dataReader["Address"]);
                    }
                }
                connection.Close();
            }
            return View(hospital);
        }

        public IActionResult Doctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Doctor";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Doctor doctor = new Doctor();
                        doctor.Id = Convert.ToInt32(dataReader["Id"]);
                        doctor.Name = Convert.ToString(dataReader["Name"]);
                        doctor.Speciality = Convert.ToString(dataReader["Speciality"]);
                        doctor.Hospital_id = Convert.ToInt32(dataReader["Hospital_id"]);
                        doctors.Add(doctor);
                    }
                }
                connection.Close();
            }
            return View(doctors);
        }

        public IActionResult CreateDoctor(string name, string speciality, int hospital_id)
        {
            List<Hospital> hospitals = new List<Hospital>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Hospital";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Hospital hospital = new Hospital();
                        hospital.Id = Convert.ToInt32(dataReader["Id"]);
                        hospital.Name = Convert.ToString(dataReader["Name"]);
                        hospital.Address = Convert.ToString(dataReader["Address"]);
                        hospitals.Add(hospital);
                    }
                }
                connection.Close();
            }
            if (HttpContext.Request.Method == "POST")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Doctor (Name, Speciality, Hospital_id) Values ('{name}','{speciality}','{hospital_id}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Doctors");
                }
            }

            return View(hospitals);
        }

        public IActionResult UpdateDoctor(int id)
        {
            List<Hospital> hospitals = new List<Hospital>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Hospital";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Hospital hospital = new Hospital();
                        hospital.Id = Convert.ToInt32(dataReader["Id"]);
                        hospital.Name = Convert.ToString(dataReader["Name"]);
                        hospital.Address = Convert.ToString(dataReader["Address"]);
                        hospitals.Add(hospital);
                    }
                }
                connection.Close();
            }
            Doctor doctor = new Doctor();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Doctor Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        doctor.Id = Convert.ToInt32(dataReader["Id"]);
                        doctor.Name = Convert.ToString(dataReader["Name"]);
                        doctor.Speciality = Convert.ToString(dataReader["Speciality"]);
                        doctor.Hospital_id = Convert.ToInt32(dataReader["Hospital_id"]);

                        doctor.temp_hospitals = hospitals;
                    }
                }
                connection.Close();
            }
            return View(doctor);
        }

        [HttpPost]
        [ActionName("UpdateDoctor")]
        public IActionResult Update_Post(Doctor doctor)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Doctor SET Name='{doctor.Name}', Speciality='{doctor.Speciality}', Hospital_id='{doctor.Hospital_id}' Where Id='{doctor.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Doctors");
        }

        [HttpPost]
        public IActionResult DeleteDoctor(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Doctor Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }
            return RedirectToAction("Doctors");
        }

        public IActionResult DetailsDoctor(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            Doctor doctor = new Doctor();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Doctor Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        doctor.Id = Convert.ToInt32(dataReader["Id"]);
                        doctor.Name = Convert.ToString(dataReader["Name"]);
                        doctor.Speciality = Convert.ToString(dataReader["Speciality"]);
                        doctor.Hospital_id = Convert.ToInt32(dataReader["Hospital_id"]);
                    }
                }
                connection.Close();
            }
            return View(doctor);
        }

        public IActionResult Patients()
        {
            List<Patient> patients = new List<Patient>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Patient";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Patient patient = new Patient();
                        patient.Id = Convert.ToInt32(dataReader["Id"]);
                        patient.Name = Convert.ToString(dataReader["Name"]);
                        patient.Doctor_id = Convert.ToInt32(dataReader["Doctor_id"]);
                        patients.Add(patient);
                    }
                }
                connection.Close();
            }
            return View(patients);
        }

        public IActionResult CreatePatient(string name, int doctorid)
        {
            List<Doctor> doctors = new List<Doctor>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Doctor";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Doctor doctor = new Doctor();
                        doctor.Id = Convert.ToInt32(dataReader["Id"]);
                        doctor.Name = Convert.ToString(dataReader["Name"]);
                        doctor.Speciality = Convert.ToString(dataReader["Speciality"]);
                        doctors.Add(doctor);
                    }
                }
                connection.Close();
            }
            if (HttpContext.Request.Method == "POST")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Patient (Name, Doctor_id) Values ('{name}','{doctorid}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Patients");
                }
            }

            return View(doctors);
        }

        public IActionResult UpdatePatient(int id)
        {
            List<Doctor> doctors = new List<Doctor>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Doctor";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Doctor doctor = new Doctor();
                        doctor.Id = Convert.ToInt32(dataReader["Id"]);
                        doctor.Name = Convert.ToString(dataReader["Name"]);
                        doctor.Speciality = Convert.ToString(dataReader["Speciality"]);
                        doctors.Add(doctor);
                    }
                }
                connection.Close();
            }
            Patient patient = new Patient();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Patient Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        patient.Id = Convert.ToInt32(dataReader["Id"]);
                        patient.Name = Convert.ToString(dataReader["Name"]);                        
                        patient.Doctor_id = Convert.ToInt32(dataReader["Doctor_id"]);

                        patient.temp_doctors = doctors;
                    }
                }
                connection.Close();
            }
            return View(patient);
        }

        [HttpPost]
        [ActionName("UpdatePatient")]
        public IActionResult Update_Post(Patient patient)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Patient SET Name='{patient.Name}', Doctor_id='{patient.Doctor_id}' Where Id='{patient.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Patients");
        }

        [HttpPost]
        public IActionResult DeletePatient(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Patient Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }
            return RedirectToAction("Patients");
        }

        public IActionResult DetailsPatient(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            Patient patient = new Patient();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Patient Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        patient.Id = Convert.ToInt32(dataReader["Id"]);
                        patient.Name = Convert.ToString(dataReader["Name"]);
                        patient.Doctor_id = Convert.ToInt32(dataReader["Doctor_id"]);
                    }
                }
                connection.Close();
            }
            return View(patient);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
