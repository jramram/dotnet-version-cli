using System;
using System.Diagnostics;

namespace Skarpdev.DotnetVersion.Vcs.Git
{
    public class GitVcs : IVcs
    {
        /// <summary>
        /// Creates a new commit with the given message
        /// </summary>
        /// <param name="message">The message to include in the commit</param>
        public void Commit(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the current repository is clean.
        /// </summary>
        /// <returns></returns>
        public bool IsRepositoryClean()
        {
            return LaunchGitWithArgs("diff-index --quiet HEAD --");
        }

        /// <summary>
        /// Determines whether git is present in PATH on the current computer
        /// </summary>
        /// <returns></returns>
        public bool IsVcsToolPresent()
        {
            // launching `git --help` returns exit code 0 where as `git` returns 1 as git wants a cmd line argument 
            return LaunchGitWithArgs("--help");
        }

        /// <summary>
        /// Creates a new tag
        /// </summary>
        /// <param name="tagName">Name of the tag</param>
        public void Tag(string tagName)
        {
            throw new NotImplementedException();
        }

        private static bool LaunchGitWithArgs(string args, int waitForExitTimeMs = 1000, int exitCode = 0)
        {
            try
            {
                var startInfo = CreateGitShellStartInfo(args);
                var proc = Process.Start(startInfo);
                proc.WaitForExit(waitForExitTimeMs);

                return proc.ExitCode == exitCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private static ProcessStartInfo CreateGitShellStartInfo(string args)
        {
            return new ProcessStartInfo("git")
            {
                Arguments = args,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
            };
        }
    }
}