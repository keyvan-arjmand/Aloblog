using System.ComponentModel.DataAnnotations;

namespace Aloblog.Domain.Common.Enums;

public enum OrderType
{
    [Display(Name = "تعمیرات لوازم خانگی")]
    HomeApplianceRepair=1,

    [Display(Name = "تعمیرات آسانسور")]
    ElevatorRepair=2,

    [Display(Name = "تعمیرات موتور برق")]
    GeneratorRepair=3,

    [Display(Name = "لوله بازکنی و تخلیه چاه")]
    PipeUnblockingAndSepticDrain=4
}