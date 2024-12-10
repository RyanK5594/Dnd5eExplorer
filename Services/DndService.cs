using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace DndExplorer.Services
{
    public class DndService
    {
        private readonly HttpClient _httpClient;

        public DndService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ApiResult>> GetApiListAsync(string category)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiListResponse>($"https://www.dnd5eapi.co/api/{category}");
                return response?.Results ?? new List<ApiResult>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching API list: {ex.Message}");
                return new List<ApiResult>();
            }
        }


        public async Task<ApiDetailResponse> GetApiDetailAsync(string category, string index)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://www.dnd5eapi.co/api/{category}/{index}");
                return JsonSerializer.Deserialize<ApiDetailResponse>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching API detail: {ex.Message}");
                return null;
            }
        }
    }


    public class ApiListResponse
    {
        public List<ApiResult> Results { get; set; } = new List<ApiResult>();
    }


    public class ApiResult
    {
        public string Index { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class ApiDetailResponse
    {
        public string Name { get; set; }
        public int hit_die { get; set; }
        public string races { get; set; }
        public List<ProficiencyChoice> proficiency_choices { get; set; }
        public List<Proficiency> proficiencies { get; set; }
        public List<SavingThrow> saving_throws { get; set; }
        public List<Subclass> subclasses { get; set; }
        public Spellcasting spellcasting { get; set; }
        public string spells { get; set; }
        public List<string> desc { get; set; }
        public string range { get; set; }
        public List<string> components { get; set; }
        public string material { get; set; }
        public bool ritual { get; set; }
        public string duration { get; set; }
        public bool concentration { get; set; }
        public string casting_time { get; set; }
        public int level { get; set; }
        public Damage damage { get; set; }
        public School school { get; set; }
        public List<Class> classes { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string alignment { get; set; }
        public List<ArmorClass> armor_class { get; set; }
        public int hit_points { get; set; }
        public string hit_dice { get; set; }
        public string hit_points_roll { get; set; }
        public Speed speed { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int charisma { get; set; }
        public string languages { get; set; }
        public int challenge_rating { get; set; }
        public List<SpecialAbility> special_abilities { get; set; }
        public List<Action> actions { get; set; }
        public List<object> legendary_actions { get; set; }
        public List<AbilityBonuse> ability_bonuses { get; set; }
        public string size_description { get; set; }
        public List<object> starting_proficiencies { get; set; }
        public string language_desc { get; set; }
        public List<object> special { get; set; }
        public EquipmentCategory equipment_category { get; set; }
        public string category_range { get; set; }
        public Cost cost { get; set; }
        public int weight { get; set; }
        public List<Property> properties { get; set; }
        public List<object> contents { get; set; }
        public int ability_score_bonuses { get; set; }
        public int prof_bonus { get; set; }
        public string index { get; set; }
        public Class @class { get; set; }
        public LanguageOptions language_options { get; set; }
    }

    public class AbilityScore
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Choice
    {
        public string desc { get; set; }
        public int choose { get; set; }
        public string type { get; set; }
        public From from { get; set; }
    }

    public class Equipment
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class EquipmentCategory
    {
        public string name { get; set; }
        public string index { get; set; }
        public string url { get; set; }
    }

    public class From
    {
        public string option_set_type { get; set; }
        public List<Option> options { get; set; }
        public EquipmentCategory equipment_category { get; set; }
    }

    public class Info
    {
        public string name { get; set; }
        public List<string> desc { get; set; }
    }

    public class Item
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Item2
    {
        public string option_type { get; set; }
        public Choice choice { get; set; }
        public int? count { get; set; }
        public Of of { get; set; }
    }

    public class MultiClassing
    {
        public List<Prerequisite> prerequisites { get; set; }
        public List<Proficiency> proficiencies { get; set; }
    }

    public class Of
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Option
    {
        public string option_type { get; set; }
        public Item item { get; set; }
        public List<Item> items { get; set; }
        public Choice choice { get; set; }
        public int? count { get; set; }
        public Of of { get; set; }
    }

    public class Prerequisite
    {
        public AbilityScore ability_score { get; set; }
        public int minimum_score { get; set; }
    }

    public class Proficiency
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class ProficiencyChoice
    {
        public string desc { get; set; }
        public int choose { get; set; }
        public string type { get; set; }
        public From from { get; set; }
    }

    public class SavingThrow
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Spellcasting
    {
        public int level { get; set; }
        public SpellcastingAbility spellcasting_ability { get; set; }
        public List<Info> info { get; set; }
        public int spell_slots_level_1 { get; set; }
        public int spell_slots_level_2 { get; set; }
        public int spell_slots_level_3 { get; set; }
        public int spell_slots_level_4 { get; set; }
        public int spell_slots_level_5 { get; set; }
        public int spell_slots_level_6 { get; set; }
        public int spell_slots_level_7 { get; set; }
        public int spell_slots_level_8 { get; set; }
        public int spell_slots_level_9 { get; set; }
    }

    public class SpellcastingAbility
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }


    public class StartingEquipment
    {
        public Equipment equipment { get; set; }
        public int quantity { get; set; }
    }

    public class StartingEquipmentOption
    {
        public string desc { get; set; }
        public int choose { get; set; }
        public string type { get; set; }
        public From from { get; set; }
    }

    public class Subclass
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class AreaOfEffect
    {
        public string type { get; set; }
        public int size { get; set; }
    }

    public class Class
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Damage
    {
        public DamageType damage_type { get; set; }
    }

    public class DamageType
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Dc
    {
        public DcType dc_type { get; set; }
        public string dc_success { get; set; }
    }

    public class DcType
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class School
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Action
    {
        public string name { get; set; }
        public string multiattack_type { get; set; }
        public string desc { get; set; }
        public List<Action> actions { get; set; }
        public int? attack_bonus { get; set; }
        public List<Damage> damage { get; set; }
        public string action_name { get; set; }
        public int count { get; set; }
        public string type { get; set; }
    }

    public class ArmorClass
    {
        public string type { get; set; }
        public int value { get; set; }
    }

    public class Proficiency2
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Senses
    {
        public string darkvision { get; set; }
        public int passive_perception { get; set; }
    }

    public class SpecialAbility
    {
        public string name { get; set; }
        public string desc { get; set; }
    }

    public class Speed
    {
        public string walk { get; set; }
    }

    public class AbilityBonuse
    {
        public AbilityScore ability_score { get; set; }
        public int bonus { get; set; }
    }

    public class Cost
    {
        public int quantity { get; set; }
        public string unit { get; set; }
    }

    public class Property
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Language
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class LanguageOptions
    {
        public int choose { get; set; }
        public string type { get; set; }
        public From from { get; set; }
    }
}
