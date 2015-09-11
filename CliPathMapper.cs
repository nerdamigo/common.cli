using NerdAmigo.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdAmigo.Common.Cli
{
    public class CliPathMapper : IPathMapper
    {
		public string MapPath(string appRelativePath)
		{
			if (String.IsNullOrWhiteSpace(appRelativePath))
			{
				throw new ArgumentNullException("appRelativePath");
			}

			if (!appRelativePath.StartsWith("~/"))
			{
				return appRelativePath;
			}

			string executionEntryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			string combined = Path.Combine(executionEntryPath, appRelativePath.Substring(2));
			if (!combined.StartsWith(executionEntryPath))
			{
				throw new ArgumentException("Mapped Path resolved to a directory outside the applications's entry point", "appRelativePath");
			}

			return combined;
		}
	}
}
