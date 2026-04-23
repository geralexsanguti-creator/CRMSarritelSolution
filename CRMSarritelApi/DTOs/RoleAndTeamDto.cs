namespace CRMSarritelApi.DTOs
{
    public class RoleAndTeamDto
    {
        public string? Departamento { get; set; }
        public string? Puesto { get; set; }
        public int RolId { get; set; }
        public List<int>? EquipoIds { get; set; }
    }
}
