using DatabaseModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace WorkflowLab
{
    class Program
    {
        static WorkflowDbContext context = new WorkflowDbContext();
        static async Task Main(string[] args)
        {
            //await PrepareData(); 
            await TestWorkflow();
            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static async Task TestWorkflow()
        {
            Person person = await context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Account == "001");
            string title = "送審主題" + Guid.NewGuid().ToString();
            Request request = new Request()
            {
                Title = title,
                Description = "申請說明",
                CurrentSigningLevel = 1,
                Note = "",
                PersonId = person.Id,
                Status = SigningStatus.Sending,
                Person = null,
            };
            context.Entry(request).State = EntityState.Added;
            await context.SaveChangesAsync();

            #region 第1階主管審核
            foreach (var fooxx in context.Set<Request>().Local)
            {
                context.Entry(fooxx).State = EntityState.Detached;
            }
            Person person2 = await context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Account == "002");
            Request currentRequest = await context.Requests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Title == title);
            currentRequest.FinalSigningPersonId = person2.Id;
            currentRequest.CurrentSigningLevel++;
            currentRequest.Status = SigningStatus.Approve;
            context.Entry(currentRequest).State = EntityState.Modified;
            await context.SaveChangesAsync();
            #endregion

            #region 第2階主管審核
            foreach (var fooxx in context.Set<Request>().Local)
            {
                context.Entry(fooxx).State = EntityState.Detached;
            }
            Person person3 = await context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Account == "003");
            Request currentRequest2 = await context.Requests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Title == title);
            currentRequest2.FinalSigningPersonId = person3.Id;
            currentRequest2.CurrentSigningLevel++;
            currentRequest2.Status = SigningStatus.Approve;
            context.Entry(currentRequest2).State = EntityState.Modified;
            await context.SaveChangesAsync();
            #endregion

            #region 第3階主管審核
            foreach (var fooxx in context.Set<Request>().Local)
            {
                context.Entry(fooxx).State = EntityState.Detached;
            }
            Person person4 = await context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Account == "003");
            Request currentRequest3 = await context.Requests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Title == title);
            currentRequest3.FinalSigningPersonId = person4.Id;
            currentRequest3.CurrentSigningLevel++;
            currentRequest3.Status = SigningStatus.Approve;
            context.Entry(currentRequest3).State = EntityState.Modified;
            await context.SaveChangesAsync();
            #endregion
        }

        private static async Task PrepareData()
        {
            #region Prepare Data
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;

            #region Generate Person
            Person person1 = new Person()
            {
                Account = "001",
            };
            context.Entry(person1).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            Person person2 = new Person()
            {
                Account = "002",
            };
            context.Entry(person2).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            Person person3 = new Person()
            {
                Account = "003",
            };
            context.Entry(person3).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            Person person4 = new Person()
            {
                Account = "004",
            };
            context.Entry(person4).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await context.SaveChangesAsync();
            #endregion

            #region Generate Polcy
            Policy policy = new Policy()
            {
                Name = "標準簽核流程",
            };
            context.Entry(policy).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await context.SaveChangesAsync();
            #endregion

            #region Generate PolcyDetail
            PolicyDetail detail1 = new PolicyDetail()
            {
                Level = 1,
                Manager = person2.Id,
                Name = "第一階層審核",
                PolicyId = policy.Id,
            };
            context.Entry(detail1).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            PolicyDetail detail2 = new PolicyDetail()
            {
                Level = 2,
                Manager = person3.Id,
                Name = "第2階層審核",
                PolicyId = policy.Id,
            };
            context.Entry(detail2).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            PolicyDetail detail3 = new PolicyDetail()
            {
                Level = 3,
                Manager = person4.Id,
                Name = "第3階層審核",
                PolicyId = policy.Id,
            };
            context.Entry(detail3).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await context.SaveChangesAsync();
            #endregion
            #endregion
        }
    }
}
