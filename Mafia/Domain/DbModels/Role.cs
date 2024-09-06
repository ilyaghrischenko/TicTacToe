using Domain.Enums;

namespace Domain.DbModels;

public class Role
{
    public int Id { get; set; }
    public RoleName RoleName { get; set; }
    public string Description { get; set; }
    
    public Role() { }
    public Role(RoleName roleName, string description)
    {
        RoleName = roleName;
        Description = description;
    }
    
    public override string ToString()
        => $"Role: {RoleName}, Description: {Description}";
}