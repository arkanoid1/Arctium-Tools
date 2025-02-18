﻿/*
 * Copyright (C) 2012-2014 Arctium Emulation <http://arctium.org>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Awps.Log
{
    class FileLog : IDisposable
    {
        public string LogFile { get; set; }
        FileStream logStream;

        public FileLog(string directory, string name)
        {
            SetLogFile(directory, name);
        }

        public void SetLogFile(string directory, string name)
        {
            LogFile = string.Format("{0}/{1}_{2} - {3}.awps", directory, Globals.FullVersion, Helper.GetCustomTimeFormat("MMMM-dd-Hmm"), name);

            logStream = new FileStream(LogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 4096, true);
        }

        public async Task Write(string logMessage)
        {
            var logBytes = Encoding.UTF8.GetBytes(logMessage + "\r\n");

            await logStream.WriteAsync(logBytes, 0, logBytes.Length);
            await logStream.FlushAsync();
        }

        public void Dispose()
        {
            LogFile = "";

            logStream.Flush();
            logStream.Dispose();
        }
    }
}
