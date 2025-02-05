using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideBar.Components.Models;

public class MenuItem
{
    public string Title { get; set; } // Menu item title
    public string Icon { get; set; } // Font Awesome icon class
    public string Link { get; set; } // URL or route
    public List<MenuItem> SubMenu { get; set; } = new(); // Submenu items
    public bool IsActive { get; set; } // Active state
}