namespace TourPortal.Models;

using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("tours")]
public class TourDbModel : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }

    [Column("slug")]
    public string Slug { get; set; } = string.Empty;

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("location")]
    public string Location { get; set; } = string.Empty;

    [Column("country")]
    public string Country { get; set; } = string.Empty;

    [Column("category")]
    public string Category { get; set; } = string.Empty;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("rating")]
    public double Rating { get; set; }

    [Column("review_count")]
    public int ReviewCount { get; set; }

    [Column("duration")]
    public string Duration { get; set; } = string.Empty;

    [Column("image_url")]
    public string ImageUrl { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("group_size")]
    public int GroupSize { get; set; } = 8;

    [Column("office_id")]
    public string? OfficeId { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
