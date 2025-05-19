namespace App.App
{
    public class MissionModifiers : IReadOnlyMissionModifiers
    {
        public bool HasFriendlyFire { get; set; } = true;
        public float DamageScale { get; set; } = 1;
        public bool AutoReloading { get; set; } = true;
        public bool DropMagazineOnReloading { get; set; } = false;
    }

    public interface IReadOnlyMissionModifiers
    {
        public bool HasFriendlyFire { get; }
        public float DamageScale { get; }
        public bool AutoReloading { get; }
        public bool DropMagazineOnReloading { get; }
    }
}