using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetCore;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    [Required(ErrorMessage = "Please enter a name for the machine.")]
    public string Model { get; set; }
    public List<EngineerMachine> JoinEntities { get; set; }
  }
}