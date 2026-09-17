using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TravellerTools.Fundamentals;
using TravellerTools.TravellerData;

namespace TravellerTools.CharGen;

/// <summary>Resolves skill selections from a character's service tables.</summary>
/// <remarks>Create and interact with the form on its owning Windows Forms UI thread.</remarks>
public partial class SkillSelectionDialog : Form, ISkillSpecialisationCollection
{
    // The prompt displays the number of unspent skill selections.
    private const string SkillsRemainingFormat = "Skill Selections Remaining ... {0}";

    // protected members

    /// <summary>The service whose tables supply the available outcomes.</summary>
    protected TravellerService service;
    /// <summary>The character mutated when a selection is resolved.</summary>
    protected TravellerCharacter character;
    /// <summary>The number of skill selections still available.</summary>
    protected decimal skillCount;

    // Public Constructors

    /// <summary>Initializes service-table choices for a character.</summary>
    /// <param name="service">The non-null career supplying skill tables.</param>
    /// <param name="character">The non-null character to receive skills or characteristic adjustments.</param>
    /// <param name="skillCount">The number of selections to resolve; zero closes the dialog.</param>
    public SkillSelectionDialog(TravellerService service, TravellerCharacter character, decimal skillCount)
    {
        this.service = service;
        this.character = character;
        this.skillCount = skillCount;
        InitializeComponent();
        UpdateButtons();
    }

    /// <summary>Refreshes table choices and remaining selections, closing when none remain.</summary>
    /// <remarks>The second advanced-education table is enabled only for EDU of at least eight.</remarks>
    protected void UpdateButtons()
    {
        if (skillCount == 0)
        {
            // If nothing left to do, close the form
            Close();
        }
        else
        {
            skillsRemainingLabel.Text = string.Format(SkillsRemainingFormat, skillCount);
            characterDisplay.Text = character.ShortStringFormat();
            skillTable1Button.Text = service.PersonalDevelopmentTableText();
            skillTable2Button.Text = service.ServiceSkillsTableText();
            skillTable3Button.Text = service.AdvancedEducationTableText();
            skillTable4Button.Text = service.AdvancedEducationTable2Text();

            // Only enabled the 2nd Advanced Education Table, if the character's education is 8 or more
            skillTable4Button.Enabled = character.EDU > 7;
        }
    }

    /// <summary>Rolls 1d6 and applies the first matching table adjustment to the character.</summary>
    /// <param name="table">The non-null list mapping roll totals to adjustments.</param>
    /// <remarks>Consumes one selection only when a table entry matches.</remarks>
    protected void ProcessSkillSelection(List<KeyValuePair<int, TravellerSkillModifier>> table)
    {
        int roll = DiceTools.RollOneDie(6);
        TravellerSkillModifier? rolledSkill = null;
        foreach (KeyValuePair<int, TravellerSkillModifier> item in table)
        {
            if (roll == item.Key)
            {
                rolledSkill = item.Value;
                break;
            }
        }
        if (rolledSkill != null)
        {
            character.AddSkill(rolledSkill, this);
            skillCount--;
        }
        UpdateButtons();
    }

    // Implementation of ISkillSpecialisationCollection

    /// <summary>Displays a modal choice of specialisations.</summary>
    /// <param name="skillName">The parent skill name shown in the prompt.</param>
    /// <param name="list">The non-null, non-empty list of available choices.</param>
    /// <returns>The initially selected child, or null if the dialog has no selected skill.</returns>
    /// <remarks>Returns one choice only; the character requests any further specialisations. The non-nullable return annotation is retained for source compatibility.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">The choice list is empty, so the dialog cannot select its first item.</exception>
    public TravellerSkill SelectSpecialisation(string skillName, List<TravellerSkill> list)
    {
        using SelectSkillSpecialisationForm form = new(skillName, list);
        form.ShowDialog();
        return form.SelectedSkill!;
    }

    // Private Event Handlers

    // Resolves one selection from the personal-development table.
    private void skillTable1Button_Click(object sender, EventArgs e)
    {
        ProcessSkillSelection(service.PersonalDevelopmentTable);
    }

    // Resolves one selection from the service-skills table.
    private void skillTable2Button_Click(object sender, EventArgs e)
    {
        ProcessSkillSelection(service.ServiceSkillsTable);
    }

    // Resolves one selection from the first advanced-education table.
    private void skillTable3Button_Click(object sender, EventArgs e)
    {
        ProcessSkillSelection(service.AdvancedEducationTable);
    }

    // Resolves one selection from the education-gated advanced table.
    private void skillTable4Button_Click(object sender, EventArgs e)
    {
        ProcessSkillSelection(service.AdvancedEducationTable2);
    }
}
