﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophmores_FinalProj
{
  public class Item
  {
    public string name { get; set; }
    public string type { get; set; }
    public string description { get; set; }
    public Item(string Name, string Type, string Description)
    {
      name = Name;
      type = Type;
      description = Description;
    }
    public Item()
    {
      this.name = "ItemHasNoName";
    }
  }
}