﻿using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace ZooExpoOrg.Services.EmailService;

public class EmailService : IEmailService
{
	public async Task SendEmailAsync(string email, string subject, string message)
	{
		var emailMessage = new MimeMessage();

		emailMessage.From.Add(new MailboxAddress("Администрация сайта ZooExpoOrg", "admin@metanit.com"));
		emailMessage.To.Add(new MailboxAddress("", email));
		emailMessage.Subject = subject;
		emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
		{
			Text = message
		};

		using (var client = new SmtpClient())
		{
			await client.ConnectAsync("smtp.gmail.com", 587, false);
			await client.AuthenticateAsync("dmitriydavidenkokeltt@gmail.com", "ifjbdklfmyvuiiqs");
			await client.SendAsync(emailMessage);

			await client.DisconnectAsync(true);
		}
	}
}