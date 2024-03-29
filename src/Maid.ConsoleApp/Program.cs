﻿// Maid
// Copyright (C) 2023 Pet Sedláček
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using Maid.ConsoleApp;
using Maid.Core;
using Maid.Core.Boundaries;
using Maid.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true);
                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<MaidOptions>(hostContext.Configuration);
                services.AddTransient<IFileSystem, OsFileSystem>();
                services.AddTransient(typeof(IMaidLogger<>), typeof(MicrosoftMaidLogger<>));
                services.AddTransient<IFileCompression, WindowsFileCompression>();
                services.AddTransient<FileDecompressor>();
                services.AddTransient<DirectoryFlattener>();
                services.AddHostedService<MaidApp>();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConsole();
            });

builder.Build().Run();
