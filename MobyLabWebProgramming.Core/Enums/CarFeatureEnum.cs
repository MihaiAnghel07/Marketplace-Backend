using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Enums;

[JsonConverter(typeof(SmartEnumNameConverter<CarFeatureEnum, string>))]

public sealed class CarFeatureEnum : SmartEnum<CarFeatureEnum, string>
{
    public static readonly CarFeatureEnum Climatronic = new(nameof(Climatronic), "Climatronic");
    public static readonly CarFeatureEnum FaruriLed = new(nameof(FaruriLed), "FaruriLed");
    public static readonly CarFeatureEnum Trapa = new(nameof(Trapa), "Trapa");
    public static readonly CarFeatureEnum ACC = new(nameof(ACC), "ACC");
    public static readonly CarFeatureEnum AndroidAuto = new(nameof(AndroidAuto), "AndroidAuto");
    public static readonly CarFeatureEnum Navigatie = new(nameof(Navigatie), "Navigatie");
    public static readonly CarFeatureEnum TapiseriePiele = new(nameof(TapiseriePiele), "TapiseriePiele");
    public static readonly CarFeatureEnum Camera360 = new(nameof(Camera360), "Camera360");
    public static readonly CarFeatureEnum SenzoriParcare = new(nameof(SenzoriParcare), "SenzoriParcare");
    public static readonly CarFeatureEnum LaneAssist = new(nameof(LaneAssist), "LaneAssist");
    public static readonly CarFeatureEnum Isofix = new(nameof(Isofix), "Isofix");




    private CarFeatureEnum(string name, string value) : base(name, value)
    {
    }
}
