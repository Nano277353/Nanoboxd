﻿using Classes;

string banner = File.ReadAllText("Header.txt");

Console.WriteLine(banner);

Console.WriteLine("Please enter your username and password:");

User user = new User();

user.EnterCredentials();

Console.WriteLine($"Welcome, {user.Username}!");

