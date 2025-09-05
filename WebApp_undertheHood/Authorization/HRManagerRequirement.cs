using System;
using Microsoft.AspNetCore.Authorization;

namespace WebApp_undertheHood.Authorization;

public class HRManagerRequirement(int probationPeriodInMonths) : IAuthorizationRequirement
{
    public int ProbationPeriodInMonths { get; } = probationPeriodInMonths; // Approximate conversion
}

public class HRManagerRequirementHandler : AuthorizationHandler<HRManagerRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerRequirement requirement)
    {
        // Implement your authorization logic here
        if (!context.User.HasClaim(c => c.Type == "EmploymentDate"))
        {
            return Task.CompletedTask;
        }

        if (DateTime.TryParse(context.User.FindFirst(c => c.Type == "EmploymentDate")?.Value, out DateTime employmentDate))
        {
            // Check if the employee is past their probation period
            var period = DateTime.Now - employmentDate;

            if (period.Days > 30 * requirement.ProbationPeriodInMonths)
            {
                context.Succeed(requirement);
            }
        }

       return Task.CompletedTask;
    }
}
