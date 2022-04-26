using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using IpdmsJob.Models;
using IpdmsJob.Models.AppDbContext;

namespace IpdmsJob
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;
        private IpdmsDbContext _context;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            _logger.LogInformation("The service has been stopped...");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var optionsBuilder = new DbContextOptionsBuilder<IpdmsDbContext>();
                optionsBuilder.UseSqlServer(Constants.Constants.connString);
                _context = new IpdmsDbContext(optionsBuilder.Options);

                var records = await (from p in _context.Projects
                                    join u in _context.IpdmsUsers on p.ipdms_user_id equals u.ipdms_user_id
                                    join d in _context.Documents on p.project_id equals d.project_id
                                    join oa in _context.OfficeActions on d.office_action_id equals oa.office_action_id
                                    where p.is_deleted == false
                                    && d.is_deleted == false
                                    && d.office_action_id != 1
                                    && d.mail_date != null
                                    && d.response_date == null
                                    select new
                                    {
                                        email = u.email,
                                        agent = u.first_name,
                                        projectId = p.project_id,
                                        applicationNo = p.application_no,
                                        mailDate = d.mail_date,
                                        applicantName = p.applicant_name,
                                        doucumentId = d.document_id,
                                        mail_date = d.mail_date,
                                        projectName = p.project_title,
                                        officeAction = oa.office_action_name1,
                                        dueDaysRemaining = (d.mail_date.Value.AddDays(oa.office_action_due ?? 0) - DateTime.Today.Date).Days,

                                    }).ToListAsync();

                var dueInFiveDays = records.Where(d => d.dueDaysRemaining == 5).ToList();

                var dueInThreeDays = records.Where(d => d.dueDaysRemaining == 3).ToList();

                var dueInOneDay = records.Where(d => d.dueDaysRemaining == 1).ToList();


                if(dueInFiveDays.Count > 0)
                {
                    //var emailSent5 = await _context.EmailSents.Where(s => s.day_five == true).ToListAsync();
                    foreach (var d in dueInFiveDays)
                    {
                        //if (!emailSent5.Any(x => x.document_id == d.doucumentId))
                        if (!_context.EmailSents.Any(x => x.document_id == d.doucumentId && x.day_five == true))
                        {
                            try
                            {
                                using (MailMessage mail = new MailMessage())
                                {
                                    mail.From = new MailAddress(Constants.Constants.adminEmail);
                                    mail.To.Add(d.email);
                                    mail.Subject = "IPDMS Notification";
                                    mail.Body = $"<p>Hi <b>{d.agent}</b><br><br><br>This is a reminder that your response for <b>{d.projectName} ({d.applicationNo}) - {d.officeAction}</b> is due within <b>{5} day(s)</b>.<br><br>We kindly request you not delay the response to avoid any inconvinience that will interfere with application process.<br><br>Thank you!<br><h3>Note: This is an auto-generated email, do not reply.</h3></p>";
                                    mail.IsBodyHtml = true;

                                    using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                                    {
                                        smtp.Credentials = new System.Net.NetworkCredential(Constants.Constants.adminEmail, Constants.Constants.adminPassword);
                                        smtp.EnableSsl = true;
                                        smtp.Send(mail);
                                        _logger.LogInformation($"Mail Sent! - {d.applicationNo} (Due within 5 days)");
                                    }
                                }

                                if (_context.EmailSents.Any(x => x.document_id == d.doucumentId && x.day_five == false))
                                {
                                    var eSent = _context.EmailSents.Where(s => s.document_id == d.doucumentId).FirstOrDefault();
                                    eSent.day_five = true;
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    var emailSent = new EmailSent()
                                    {
                                        document_id = d.doucumentId,
                                        project_id = d.projectId,
                                        day_five = true,
                                        CREATE_USER_DATE = DateTime.Now
                                    };

                                    _context.EmailSents.Add(emailSent);
                                    await _context.SaveChangesAsync();
                                }
                                
                            }catch(Exception ex)
                            {
                                _logger.LogError(ex.ToString());
                            }
                        }
                    }
                }

                //////////////////////////////Day 3
                if (dueInThreeDays.Count > 0)
                {
                    //var emailSent3 = await _context.EmailSents.Where(s => s.day_three == true).ToListAsync();
                    foreach (var d in dueInThreeDays)
                    {
                        //if (!emailSent3.Any(x => x.document_id == d.doucumentId))
                        if (!_context.EmailSents.Any(x => x.document_id == d.doucumentId && x.day_three == true))
                        {
                            try
                            {
                                using (MailMessage mail = new MailMessage())
                                {
                                    mail.From = new MailAddress(Constants.Constants.adminEmail);
                                    mail.To.Add(d.email);
                                    mail.Subject = "IPDMS Notification";
                                    mail.Body = $"<p>Hi <b>{d.agent}</b><br><br><br>This is a reminder that your response for <b>{d.projectName} ({d.applicationNo}) - {d.officeAction}</b> is due within <b>{3} day(s)</b>.<br><br>We kindly request you not delay the response to avoid any inconvinience that will interfere with application process.<br><br>Thank you!<br><h3>Note: This is an auto-generated email, do not reply.</h3></p>";
                                    mail.IsBodyHtml = true;

                                    using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                                    {
                                        smtp.Credentials = new System.Net.NetworkCredential(Constants.Constants.adminEmail, Constants.Constants.adminPassword);
                                        smtp.EnableSsl = true;
                                        smtp.Send(mail);
                                        _logger.LogInformation($"Mail Sent! - {d.applicationNo} (Due within 3 days)");
                                    }
                                }

                                if (_context.EmailSents.Any(x => x.document_id == d.doucumentId && x.day_three == false))
                                {
                                    var eSent = _context.EmailSents.Where(s => s.document_id == d.doucumentId).FirstOrDefault();
                                    eSent.day_three = true;
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    var emailSent = new EmailSent()
                                    {
                                        document_id = d.doucumentId,
                                        project_id = d.projectId,
                                        day_three = true,
                                        CREATE_USER_DATE = DateTime.Now
                                    };

                                    _context.EmailSents.Add(emailSent);
                                    await _context.SaveChangesAsync();
                                }

                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex.ToString());
                            }
                        }
                    }
                }
                //////////////////////////////Day 1
                if (dueInOneDay.Count > 0)
                {
                    //var emailSent1 = await _context.EmailSents.Where(s => s.day_one == true).ToListAsync();
                    foreach (var d in dueInOneDay)
                    {
                        if (!_context.EmailSents.Any(x => x.document_id == d.doucumentId && x.day_one == true))
                        {
                            try
                            {
                                using (MailMessage mail = new MailMessage())
                                {
                                    mail.From = new MailAddress(Constants.Constants.adminEmail);
                                    mail.To.Add(d.email);
                                    mail.Subject = "IPDMS Notification";
                                    mail.Body = $"<p>Hi <b>{d.agent}</b><br><br><br>This is a reminder that your response for <b>{d.projectName} ({d.applicationNo}) - {d.officeAction}</b> is due within <b>{1} day</b>.<br><br>We kindly request you not delay the response to avoid any inconvinience that will interfere with application process.<br><br>Thank you!<br><h3>Note: This is an auto-generated email, do not reply.</h3></p>";
                                    mail.IsBodyHtml = true;

                                    using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                                    {
                                        smtp.Credentials = new System.Net.NetworkCredential(Constants.Constants.adminEmail, Constants.Constants.adminPassword);
                                        smtp.EnableSsl = true;
                                        smtp.Send(mail);
                                        _logger.LogInformation($"Mail Sent! - {d.applicationNo} (Due within 1 day)");
                                    }
                                }

                                if (_context.EmailSents.Any(x => x.document_id == d.doucumentId && x.day_one == false))
                                {
                                    var eSent = _context.EmailSents.Where(s => s.document_id == d.doucumentId).FirstOrDefault();
                                    eSent.day_one = true;
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    var emailSent = new EmailSent()
                                    {
                                        document_id = d.doucumentId,
                                        project_id = d.projectId,
                                        day_one = true,
                                        CREATE_USER_DATE = DateTime.Now
                                    };

                                    _context.EmailSents.Add(emailSent);
                                    await _context.SaveChangesAsync();
                                }

                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex.ToString());
                            }
                        }
                    }
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
