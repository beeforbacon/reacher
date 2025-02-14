﻿using Reacher.Common.Models;

namespace Reacher.Common.Logic;

public interface IEmailContentRenderer
{
    Task<string> GetFowardEmail(string body, string fromEmail, string? fromName, string subject, decimal amount, string reacherEmail);
    Task<string> GetPayEmailBody(string reacherEmail, string reacherName, Guid emailId, string subject, decimal cost, string html, int attachmentCount);
    Task<string> GetPaymentSuccessEmailBody(string reacherEmail, string reacherName, string subject, decimal cost, string html, int attachmentCount);
}

public class EmailContentRenderer : IEmailContentRenderer
{
    private readonly ITemplateService _templateService;

    public EmailContentRenderer(ITemplateService templateService)
    {
        _templateService = templateService;
    }
    public Task<string> GetPaymentSuccessEmailBody(string reacherEmail, string reacherName, string subject, decimal cost, string html, int attachmentCount)
    {
        var email = new ReacherEmail
        {
            Summary = $@"{reacherName} has received your email.",
            MarkdownContentItems = new[] {
        $@"Hey there,

We just wanted to let you know that your payment of **{cost:c}** was received, and Reacher has delivered your email to **{reacherName}**."
            },
            OtherEmail = new() { Html = html, Subject = subject, Label = "**Your Email:**", AttachmentCount = attachmentCount },
            MarkdownContentItemsAfter = new[] {
                @$"Please note that **all payments go directly to {reacherName}**. 
 
A direct response to this email will be *ignored*."
            },
            BottomNote = $"We are sending you this email because you sent an email to **{reacherEmail}**, and {reacherName} has chosen to use Reacher to prevent spam.",
            Invite = true
        };
        return _templateService.GetTemplateHtmlAsStringAsync("Main", new RazorReacherEmail(email));
    }

    public Task<string> GetPayEmailBody(string reacherEmail, string reacherName, Guid emailId, string subject, decimal cost, string html, int attachmentCount)
    {
        var email = new ReacherEmail
        {
            Summary = $@"Reacher has received your email. Please pay the spam prevention invoice to get your email delivered.",
            MarkdownContentItems = new[] {
        $@"Hey there,

We see you wanted to reach **{reacherName}**, but your email hasn't been delivered yet.

**{reacherName}** uses Reacher to prevent spam from entering their inbox.

All you gotta do is add a **{cost:c}** Lightning Network payment to prove you are not sending spam. Reacher will then deliver your email below.

If you do not have a way to pay a Lightning Network invoice, we recommend the Strike app. For more information click here: <a href="https://strike.me">Strike</a>"
            },
            OtherEmail = new() { Html = html, Subject = subject, Label = "**Your Email:**", AttachmentCount = attachmentCount },
            Actions = new List<EmailAction> {
                new(){ActionName = "Add a Tip Now", Url = "https://www.reacher.me/tip/" + emailId}
            },
            MarkdownContentItemsAfter = new[] {
                @$"Please note that **all payments go directly to {reacherName}**. 
  
{reacherName} is constantly bombarded by emails, so if you want to get their attention, add a tip! 
  
Also note that a direct response to this email will be *ignored*."
            },
            BottomNote = $"We are sending you this email because you sent an email to **{reacherEmail}**, and {reacherName} has chosen to use Reacher to prevent spam.",
            Invite = true
        };
        return _templateService.GetTemplateHtmlAsStringAsync("Main", new RazorReacherEmail(email));// _contentProvider.RunTemplate(email);
    }
    public Task<string> GetFowardEmail(string body, string fromEmail, string? fromName, string subject, decimal amount, string reacherEmail)
    {
        var from = $@"{ fromName ?? fromEmail }{ (fromName != null ? $" ({fromEmail})" : "") }";
        var email = new ReacherEmail
        {
            Summary = $"You have a new Reacher email from {from}",
            MarkdownContentItems = new[] {
                $@"You got tipped **{amount:C}** to receive the email below.

You may reply directly to this email, and your response will be forwarded to the sender."
            },
            OtherEmail = new() { Html = body, From = from, Subject = subject, Label = "**Their Email:**" },
            MarkdownContentItemsAfter = new[] {
            $"If you reply to this email, your message will be forwarded through **{reacherEmail}** without divulging your actual email address"
            }
        };
        return _templateService.GetTemplateHtmlAsStringAsync("Main", new RazorReacherEmail(email));// _contentProvider.RunTemplate(email);
    }
}

