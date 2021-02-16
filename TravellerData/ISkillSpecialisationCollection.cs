using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellerTools.TravellerData
{
    public interface ISkillSpecialisationCollection
    {
        TravellerSkill SelectSpecialisation(string skillName, List<TravellerSkill> list);
    }
}
