using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SinavYonetimUygulamasi.Context;
using SinavYonetimUygulamasi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SinavYonetimUygulamasi.Controllers;

[Authorize(Roles = "Egitmen,Admin")]
public class CourseController : Controller
{
    private readonly AppDbContext _context;

    public CourseController(AppDbContext context)
    {
        _context = context;
    }

    // List all courses
    public async Task<IActionResult> CourseList()
    {
        var courses = await _context.Courses
            .Include(c => c.Topics)
            .ToListAsync();
        return View(courses);
    }

    // Course create form
    public IActionResult Create()
    {
        return View();
    }

    // Save new course
    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        if (ModelState.IsValid)
        {
            course.CreatedAt = DateTime.Now;
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Ders başarıyla eklendi.";
            return RedirectToAction("CourseList");
        }
        return View(course);
    }

    // Course edit form
    public async Task<IActionResult> Edit(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // Update course
    [HttpPost]
    public async Task<IActionResult> Edit(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("CourseList");
        }
        return View(course);
    }

    // Delete course confirmation
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // Confirm delete course
    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("CourseList");
    }

    // Select a course before adding a topic
    public async Task<IActionResult> SelectCourseForTopic()
    {
        var courses = await _context.Courses.ToListAsync();
        return View(courses);
    }

    // Manage topics for a course
    public async Task<IActionResult> ManageTopics(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Topics)
            .ThenInclude(t => t.SubTopics)
            .FirstOrDefaultAsync(c => c.Id == id);
            
        if (course == null)
        {
            return NotFound();
        }
        
        ViewBag.CourseId = id;
        ViewBag.CourseName = course.Name;
        
        return View(course.Topics);
    }

    // Add topic form
    public IActionResult AddTopic(int courseId)
    {
        ViewBag.CourseId = courseId;
        return View();
    }

    // Save new topic
    [HttpPost]
    public async Task<IActionResult> AddTopic(IFormCollection form)
    {
        try
        {
            // Form verilerinden ID ve ad değerlerini al
            int courseId = int.Parse(form["CourseId"]);
            string name = form["Name"];

            // Hata kontrolü
            if (string.IsNullOrEmpty(name))
            {
                TempData["ErrorMessage"] = "Konu adı boş olamaz.";
                ViewBag.CourseId = courseId;
                return View();
            }

            // Yeni konu oluştur
            var topic = new Topic
            {
                CourseId = courseId,
                Name = name,
            };

            // Veritabanına kaydet
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Konu başarıyla eklendi.";
            return RedirectToAction("ManageTopics", new { id = courseId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Konu eklenirken bir hata oluştu: " + ex.Message;
            return View();
        }
    }

    // Delete topic 
    [HttpPost]
    public async Task<IActionResult> DeleteTopic(int id)
    {
        var topic = await _context.Topics.FindAsync(id);
        if (topic != null)
        {
            var courseId = topic.CourseId;
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageTopics", new { id = courseId });
        }
        return NotFound();
    }

    // Manage subtopics for a topic
    public async Task<IActionResult> ManageSubtopics(int id)
    {
        var topic = await _context.Topics
            .Include(t => t.SubTopics)
            .Include(t => t.Course)
            .FirstOrDefaultAsync(t => t.Id == id);
            
        if (topic == null)
        {
            return NotFound();
        }
        
        ViewBag.TopicId = id;
        ViewBag.TopicName = topic.Name;
        ViewBag.CourseId = topic.CourseId;
        ViewBag.CourseName = topic.Course.Name;
        
        return View(topic.SubTopics);
    }

    // Add subtopic form
    public async Task<IActionResult> AddSubtopic(int topicId)
    {
        // Topik bilgisini al (CourseId için)
        var topic = await _context.Topics
            .Include(t => t.Course)
            .FirstOrDefaultAsync(t => t.Id == topicId);
            
        if (topic == null)
        {
            return NotFound();
        }
        
        ViewBag.TopicId = topicId;
        ViewBag.TopicName = topic.Name;
        ViewBag.CourseId = topic.CourseId;
        ViewBag.CourseName = topic.Course.Name;
        
        return View();
    }

    // Save new subtopic
    [HttpPost]
    public async Task<IActionResult> AddSubtopic(IFormCollection form)
    {
        try 
        {
            // Form verilerinden ID ve ad değerlerini al
            int topicId = int.Parse(form["TopicId"]);
            string name = form["Name"];

            // Hata kontrolü
            if (string.IsNullOrEmpty(name))
            {
                TempData["ErrorMessage"] = "Alt konu adı boş olamaz.";
                ViewBag.TopicId = topicId;
                return View();
            }

            // Topik bilgisini al (CourseId için)
            var topic = await _context.Topics.FindAsync(topicId);
            if (topic == null)
            {
                TempData["ErrorMessage"] = "Konu bulunamadı.";
                return RedirectToAction("CourseList");
            }

            // Yeni alt konu oluştur
            var subtopic = new SubTopic
            {
                TopicId = topicId,
                Name = name,
                CreatedAt = DateTime.Now
            };

            // Veritabanına kaydet
            await _context.SubTopics.AddAsync(subtopic);
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Alt konu başarıyla eklendi.";
            ViewBag.CourseId = topic.CourseId; // ManageSubtopics görünümü için CourseId lazım
            return RedirectToAction("ManageSubtopics", new { id = topicId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Alt konu eklenirken bir hata oluştu: " + ex.Message;
            return View();
        }
    }

    // Delete subtopic
    [HttpPost]
    public async Task<IActionResult> DeleteSubtopic(int id)
    {
        var subtopic = await _context.SubTopics.FindAsync(id);
        if (subtopic != null)
        {
            var topicId = subtopic.TopicId;
            _context.SubTopics.Remove(subtopic);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageSubtopics", new { id = topicId });
        }
        return NotFound();
    }
}