﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IntroCS;

namespace Sophmores_FinalProj.Utilities
{
  /// <summary>
  /// Holds Utility code for manipulating the console window
  /// </summary>
  public class TextUtil
  {
    /// <summary>
    /// Prints standard Press Any Key to Continue Message
    /// Then clears the screen
    /// </summary>
    public static void PressAnyKeyBufferClear()
    {
      string message = ("\nPress any key to continue...");
      Console.WriteLine(message);
      Console.ReadKey();
      Console.Clear();
    }
    /// <summary>
    /// Prints custom Press Any Key to Continue message
    /// Then clears the screen
    /// </summary>
    /// <param name="message">Message to display to player</param>
    public static void PressAnyKeyBufferClear(string message)
    {
      Console.WriteLine("\n" + message);
      Console.ReadKey();
      Console.Clear();
    }
    /// <summary>
    /// Sets Buffer Size (within the console that is in the output)
    /// </summary>
    public static void SetBufferSize()
    {
      if (CheckOS())
      {
        Console.BufferHeight = (Int16.MaxValue - 1);
        Console.BufferWidth = (80);
      }
    }
    /// <summary>
    /// Checks user OS platform
    /// </summary>
    /// <returns>True if Windows, false otherwise</returns>
    private static bool CheckOS()
    {
      OperatingSystem os = Environment.OSVersion;
      string platform = os.Platform.ToString();
      if (platform.ToLower().StartsWith("w"))
      {
        return true;
      }
      return false;
    }
    /// <summary>
    /// Prints to Console contents of the supplied text file
    /// *Throws NotSupportedException* if param is not a '.txt'
    /// </summary>
    /// <param name="textFileToRead">
    /// The text file to be read and printed
    /// </param>
    public static void PrintTextFile(string textFileToRead)
    {
      try
      {
        string txt = ".txt";
        if (textFileToRead.EndsWith(txt))
        {
          StringBuilder builder = new StringBuilder();
          var reader = FIO.OpenReader(FIO.GetLocation(textFileToRead),
            textFileToRead);
          while (!(reader.EndOfStream))
          {

            builder.Append("\n" + reader.ReadLine());
          }
          string text = ("" + builder);
          Console.WriteLine(text);
          reader.Close();
        }
        else
        {
          throw new NotSupportedException();
        }
      }
      catch
      {
        string Location = FindTextFile(textFileToRead);
        if (Location != null)
        {
          PrintTextFileFromFullPath(Location);
        }
        else
        {
          string msg = "File not found!";
          Console.WriteLine(msg);
          throw new FileNotFoundException(msg);
        }
      }
    }
    /// <summary>
    /// Prints to Console contents of the supplied text file
    /// *Throws NotSupportedException* if param is not a '.txt'
    /// </summary>
    /// <param name="textFileToRead">
    /// The text file to be read and printed
    /// </param>
    private static void PrintTextFileFromFullPath(string textFileLocation)
    {
      try
      {
        if (!File.Exists(textFileLocation))
        {
          throw new FileNotFoundException("File Does not exist at {0}",
                                          textFileLocation);
        }
        else if (!textFileLocation.EndsWith(".txt"))
        {
          throw new NotSupportedException("That is not a text file!");
        }
        else
        {
          StringBuilder builder = new StringBuilder();
          var reader = new StreamReader(textFileLocation);
          while (!(reader.EndOfStream))
          {
            string line = LineWrap(reader);
            builder.Append("\n" + line);
          }
          string text = ("" + builder);
          Console.WriteLine(text);
          reader.Close();
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
    /// <summary>
    /// Prints to Console the contents of the supplied text
    /// File AND Returns file contents as String
    /// *Throws NotSupportedException* if param is not a '.txt'
    /// </summary>
    /// <param name="textFileToRead">
    /// The text file to be read, printed, and returned
    /// </param>
    /// <returns>Contents of the text file as String</returns>
    public static string PrintAndReturnTextFile(string textFileToRead)
    {
      string txt = ".txt";
      if (textFileToRead.EndsWith(txt))
      {
        StringBuilder builder = new StringBuilder();
        var reader = FIO.OpenReader(FIO.GetLocation(textFileToRead),
          textFileToRead);
        while (!(reader.EndOfStream))
        {

          builder.Append("\n" + reader.ReadLine());
        }
        string text = ("" + builder);
        Console.WriteLine(text);
        reader.Close();
        return text;
      }
      else
      {
        string msg = "That is not a text file!";
        Console.WriteLine(msg);
        throw new NotSupportedException(msg);
      }
    }
    /// <summary>
    /// Returns the CONTENTS of the text file.
    /// </summary>
    /// <returns>The text file.</returns>
    /// <param name="textFileToRead">Text file to read.</param>
    public static string ReturnTextFile(string textFileToRead)
    {
      string location = FindTextFile(textFileToRead);
      if (location != null)
      {
        return location;
      }
      return null;

      //string txt = ".txt";
      //if (textFileToRead.EndsWith(txt))
      //{
      //  StringBuilder builder = new StringBuilder();
      //  var reader = FIO.OpenReader(FIO.GetLocation(textFileToRead),
      //                textFileToRead);
      //  while (!(reader.EndOfStream))
      //  {

      //    builder.Append("\n" + reader.ReadLine());
      //  }
      //  string text = ("" + builder);
      //  reader.Close();
      //  return text;
      //}
      //else
      //{
      //  string msg = "That is not a text file!";
      //  Console.WriteLine(msg);
      //  throw new NotSupportedException(msg);
      //}
    }
    /// Read a long line and return it wrapped into lines.
    /// Such data is easiest generated in a regular word
    /// processor that automatically wraps lines, 
    /// so any paragraph is just stored as one long line..
    public static string LineWrap(StreamReader reader)
    {
      return WordWrap(reader.ReadLine(), 79);
    }

    private static char[] noChar = { };

    /// Split s at any number of whitespace characters.
    ///  No empty strings are inserted. 
    private static string[] SplitWhite(string s)
    {   // The function call shortens this mouthfull!
      return s.Split(noChar, StringSplitOptions.RemoveEmptyEntries);
    }

    /// Add line breaks to s so it wraps within a specified
    /// number of columns. 
    private static string WordWrap(string s, int columns)
    {
      string wrapped = "";
      s = s.Trim();
      while (s.Length > columns)
      {
        int i = s.LastIndexOf(" ", columns);
        if (i == -1)
        {
          i = columns;
        }
        wrapped += s.Substring(0, i).Trim();
        s = s.Substring(i).Trim();
        if (s.Length > 0)
        {
          wrapped += "\n";
        }
      }
      wrapped += s;
      return wrapped;
    }
    /// <summary>
    /// Searches solution & Repo for file
    /// </summary>
    /// <param name="textFile"> file to find</param>
    /// <returns>Full Path to File, or null if file not found</returns>
    private static string FindTextFile(string textFile)
    {
      string fileLocation = string.Empty;
      string[] array = { ".", "..", Path.Combine("..", "..") ,
                                    Path.Combine("..", "..", "..") , 
                                    Path.Combine("..", "..", "..", "..") };
      foreach (string topDirectory in array)
      {
        DirectoryInfo dir = new DirectoryInfo(topDirectory);
        try
        {
          if (!dir.Exists)
          {
            throw new DirectoryNotFoundException("The directory does not exist.");
          }

          // Call the GetFileSystemInfos method.
          FileSystemInfo[] infos = dir.GetFileSystemInfos();

          // Pass the result to the ListDirectoriesAndFiles 
          // method defined below.
          if (ListDirectoriesAndFiles(infos, textFile, out fileLocation))
          {
            return fileLocation;
          }
          else
          {
            continue;
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
      return null;
    }
    /// <summary>
    /// Helper function for FindTextFile
    /// </summary>
    /// <returns>True if file found, false otherwise</returns>
    private static bool ListDirectoriesAndFiles(FileSystemInfo[] FSInfo, string File, out string fileLocation)
    {
      // Check the FSInfo parameter. 
      if (FSInfo == null)
      {
        throw new ArgumentNullException("FSInfo");
      }
      // Iterate through each item. 
      foreach (FileSystemInfo i in FSInfo)
      {
        // Check to see if this is a DirectoryInfo object. 
        if (i is DirectoryInfo)
        {
          // Cast the object to a DirectoryInfo object.
          DirectoryInfo dInfo = (DirectoryInfo)i;
          // Iterate through all sub-directories.
          ListDirectoriesAndFiles(dInfo.GetFileSystemInfos(), File, out fileLocation);
        }
        // Check to see if this is a FileInfo object. 
        else if (i is FileInfo)
        {
          // Checks against passed in file
          if (i.FullName.EndsWith(File))
          {
            fileLocation = "" + i.FullName;
            return true;
          }
        }
      }
      fileLocation = string.Empty;
      return false;
    }
  }
}
