using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TravellerTools.TravellerData;

namespace SkillEditor;

/// <summary>Edits skill definitions and their nested specialisations.</summary>
/// <remarks>Create and interact with the form on its owning Windows Forms UI thread. Loading replaces the skill list; JSON null becomes an empty list. File and JSON failures propagate. Saving replaces the selected file with the current skill tree.</remarks>
public partial class SkillEditorForm : Form
{
    // static strings
    /// <summary>The initial name assigned to a newly inserted skill.</summary>
    protected static string NEW_SKILL = "New Skill";

    // Form Data

    /// <summary>The mutable list of top-level skill definitions being edited.</summary>
    protected List<TravellerSkill> Skills = new();
    /// <summary>The selected top-level skill, or null when none is selected.</summary>
    protected TravellerSkill? CurrentSkill;
    /// <summary>The selected child skill, or null when none is selected.</summary>
    protected TravellerSkill? CurrentSpecialisationSkill;

    // Public Constructors

    /// <summary>Initializes the skill editor and its selection controls.</summary>
    public SkillEditorForm()
    {
        InitializeComponent();
        Skills = new();
        UpdateBoxes();
    }

    // Protected Methods

    /// <summary>Refreshes skill and specialisation fields and enables valid list operations.</summary>
    protected void UpdateBoxes()
    {
        skillsBox.Items.Clear();
        int index = -1;
        for (int i = 0; i < Skills.Count; i++)
        {
            skillsBox.Items.Add(Skills[i]);
            if (Skills[i] == CurrentSkill)
            {
                index = i;
            }
        }

        if (Skills.Count > 0)
        {
            if (CurrentSkill == null)
            {
                CurrentSkill = Skills[0];
            }

            skillNameBox.Enabled = true;
            skillHasSpecialisationsBox.Enabled = true;
            skillSummaryBox.Enabled = true;
            skillDescriptionBox.Enabled = true;
            skillRefereeBox.Enabled = true;

            skillNameBox.Text = CurrentSkill!.Name;
            skillHasSpecialisationsBox.Checked = CurrentSkill.HasSpecialisations;
            skillSummaryBox.Text = CurrentSkill.Summary;
            skillDescriptionBox.Text = CurrentSkill.Description;
            skillRefereeBox.Text = CurrentSkill.Referee;

            if (index != -1)
            {
                suppressSkillSelectionUpdate = true;
                skillsBox.SelectedIndex = index;
                suppressSkillSelectionUpdate = false;
            }

            skillsBox.Enabled = true;
            removeSkillButton.Enabled = true;
            skillUpButton.Enabled = skillsBox.SelectedIndex != 0;
            skillDownButton.Enabled = skillsBox.SelectedIndex != Skills.Count - 1;
        }
        else
        {
            skillNameBox.Enabled = false;
            skillHasSpecialisationsBox.Enabled = false;
            skillSummaryBox.Enabled = false;
            skillDescriptionBox.Enabled = false;
            skillRefereeBox.Enabled = false;

            skillsBox.Enabled = false;
            removeSkillButton.Enabled = false;
            skillUpButton.Enabled = false;
            skillDownButton.Enabled = false;
        }

        specialisationSkillsBox.Items.Clear();
        if (CurrentSkill != null && CurrentSkill.HasSpecialisations)
        {
            index = -1;
            for (int i = 0; i < CurrentSkill.Specialisations.Count; i++)
            {
                specialisationSkillsBox.Items.Add(CurrentSkill.Specialisations[i]);
                if (CurrentSkill.Specialisations[i] == CurrentSpecialisationSkill)
                {
                    index = i;
                }
            }

            if (CurrentSkill.Specialisations.Count > 0)
            {
                if (CurrentSpecialisationSkill == null)
                {
                    CurrentSpecialisationSkill = CurrentSkill.Specialisations[0];
                }
                if (index != -1)
                {
                    suppressSpecialistSkillSelectionUpdate = true;
                    specialisationSkillsBox.SelectedIndex = index;
                    suppressSpecialistSkillSelectionUpdate = false;
                }

                specialisationSkillsBox.Enabled = true;
                specialisationSkillNameBox.Enabled = true;
                specialisationSkillSummaryBox.Enabled = true;
                specialisationSkillDescriptionBox.Enabled = true;
                specialisationSkillRefereeBox.Enabled = true;

                removeSpecialistSkillButton.Enabled = true;
                specialistSkillUpButton.Enabled = specialisationSkillsBox.SelectedIndex != 0;
                specialistSkillDownButton.Enabled = specialisationSkillsBox.SelectedIndex != CurrentSkill.Specialisations.Count - 1;
            }
            addSpecialistSkillButton.Enabled = true;

            if (CurrentSpecialisationSkill != null)
            {
                specialisationSkillNameBox.Text = CurrentSpecialisationSkill.Name;
                specialisationSkillSummaryBox.Text = CurrentSpecialisationSkill.Summary;
                specialisationSkillDescriptionBox.Text = CurrentSpecialisationSkill.Description;
                specialisationSkillRefereeBox.Text = CurrentSpecialisationSkill.Referee;
            }
            else
            {
                specialisationSkillNameBox.Text = string.Empty;
                specialisationSkillSummaryBox.Text = string.Empty;
                specialisationSkillDescriptionBox.Text = string.Empty;
                specialisationSkillRefereeBox.Text = string.Empty;
            }
        }
        else
        {
            specialisationSkillsBox.Enabled = false;
            specialisationSkillNameBox.Enabled = false;
            specialisationSkillSummaryBox.Enabled = false;
            specialisationSkillDescriptionBox.Enabled = false;
            specialisationSkillRefereeBox.Enabled = false;

            addSpecialistSkillButton.Enabled = false;
            removeSpecialistSkillButton.Enabled = false;
            specialistSkillUpButton.Enabled = false;
            specialistSkillDownButton.Enabled = false;

            specialisationSkillNameBox.Text = string.Empty;
            specialisationSkillSummaryBox.Text = string.Empty;
            specialisationSkillDescriptionBox.Text = string.Empty;
            specialisationSkillRefereeBox.Text = string.Empty;
        }
    }

    // Private Events

    // Creates a top-level definition and makes it the active editor selection.
    private void addSkillButton_Click(object sender, EventArgs e)
    {
        TravellerSkill newSkill = new();
        newSkill.Name = NEW_SKILL;
        Skills.Add(newSkill);
        CurrentSkill = newSkill;
        UpdateBoxes();
    }

    // Removes the active definition and clears the selection before rebuilding controls.
    private void removeSkillButton_Click(object sender, EventArgs e)
    {
        if (CurrentSkill != null)
        {
            Skills.Remove(CurrentSkill);
        }
        CurrentSkill = null;
        UpdateBoxes();
    }

    // Suppress selection events caused by rebuilding the top-level skill list.
    private bool suppressSkillSelectionUpdate = false;

    /// <summary>Switches the active skill and initializes its child selection without responding to internal list refreshes.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void skillsBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (skillsBox.SelectedItem is TravellerSkill && !suppressSkillSelectionUpdate)
        {
            CurrentSkill = (skillsBox.SelectedItem as TravellerSkill);
            if (!CurrentSkill!.HasSpecialisations)
            {
                CurrentSpecialisationSkill = null;
            }
            else if (CurrentSkill.Specialisations.Count > 0)
            {
                CurrentSpecialisationSkill = CurrentSkill.Specialisations[0];
            }
            UpdateBoxes();
        }
    }

    // Refreshes list labels when the active skill's display name changes.
    private void skillNameBox_TextChanged(object sender, EventArgs e)
    {
        CurrentSkill!.Name = skillNameBox.Text;
        UpdateBoxes();
    }

    // Stores the player-facing summary on the active definition.
    private void skillSummaryBox_TextChanged(object sender, EventArgs e)
    {
        CurrentSkill!.Summary = skillSummaryBox.Text;
    }

    // Stores the detailed rules text on the active definition.
    private void skillDescriptionBox_TextChanged(object sender, EventArgs e)
    {
        CurrentSkill!.Description = skillDescriptionBox.Text;
    }

    // Stores referee guidance on the active definition.
    private void skillRefereeBox_TextChanged(object sender, EventArgs e)
    {
        CurrentSkill!.Referee = skillRefereeBox.Text;
    }

    // Replaces definitions only after the selected file has been read and deserialized.
    private void LoadButton_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openDialog = new();
        openDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
        openDialog.FilterIndex = 1;
        openDialog.Multiselect = false;

        if (openDialog.ShowDialog() == DialogResult.OK)
        {
            Skills = ReadSkills(openDialog.FileName);
        }

        UpdateBoxes();
    }

    // Exports the current skill tree to the selected JSON file.
    private void saveButton_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveDialog = new();
        saveDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
        saveDialog.FilterIndex = 1;

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            WriteSkills(saveDialog.FileName, Skills);
        }
    }

    /// <summary>Reads a replacement skill tree; JSON null becomes an empty list and read or JSON errors propagate.</summary>
    /// <remarks>Omitted properties retain model defaults. No additional validation is performed.</remarks>
    private static List<TravellerSkill> ReadSkills(string fileName)
    {
        string json = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<List<TravellerSkill>>(json) ?? new();
    }

    /// <summary>Replaces the selected file with the current skill tree; serialization and write failures propagate.</summary>
    private static void WriteSkills(string fileName, List<TravellerSkill> skills)
    {
        string json = JsonSerializer.Serialize(skills);
        File.WriteAllText(fileName, json);
    }

    // Moves the selected definition earlier in the saved list order.
    private void skillUpButton_Click(object sender, EventArgs e)
    {
        int currentIndex = skillsBox.SelectedIndex;
        // Don't do anything if this is index 0 (top item)
        if (currentIndex > 0)
        {
            Skills.Reverse(currentIndex - 1, 2);
            UpdateBoxes();
        }
    }

    // Moves the selected definition later in the saved list order.
    private void skillDownButton_Click(object sender, EventArgs e)
    {
        int currentIndex = skillsBox.SelectedIndex;
        // Don't do anything if this is the last item
        if (currentIndex < Skills.Count - 1)
        {
            Skills.Reverse(currentIndex, 2);
            UpdateBoxes();
        }
    }

    // Updates whether child choices are exposed by this skill.
    private void hasSpecialisationsBox_CheckedChanged(object sender, EventArgs e)
    {
        CurrentSkill!.HasSpecialisations = skillHasSpecialisationsBox.Checked;
        UpdateBoxes();
    }

    // Adds a child definition to the current skill and selects it for editing.
    private void addSpecialistSkillButton_Click(object sender, EventArgs e)
    {
        TravellerSkill newSkill = new();
        newSkill.Name = NEW_SKILL;
        CurrentSkill!.Specialisations.Add(newSkill);
        CurrentSpecialisationSkill = newSkill;
        UpdateBoxes();
    }

    // Removes the active child definition and clears the child selection.
    private void removeSpecialistSkillButton_Click(object sender, EventArgs e)
    {
        if (CurrentSkill != null && CurrentSpecialisationSkill != null)
        {
            CurrentSkill.Specialisations.Remove(CurrentSpecialisationSkill);
        }
        CurrentSpecialisationSkill = null;
        UpdateBoxes();
    }

    // Moves a child choice earlier in its parent's specialisation list.
    private void specialistSkillUpButton_Click(object sender, EventArgs e)
    {
        int currentIndex = specialisationSkillsBox.SelectedIndex;
        // Don't do anything if this is index 0 (top item)
        if (currentIndex > 0)
        {
            CurrentSkill!.Specialisations.Reverse(currentIndex - 1, 2);
            UpdateBoxes();
        }
    }

    // Moves a child choice later in its parent's specialisation list.
    private void specialistSkillDownButton_Click(object sender, EventArgs e)
    {
        int currentIndex = specialisationSkillsBox.SelectedIndex;
        // Don't do anything if this is the last item
        if (currentIndex < CurrentSkill!.Specialisations.Count - 1)
        {
            CurrentSkill.Specialisations.Reverse(currentIndex, 2);
            UpdateBoxes();
        }
    }

    // Suppress selection events caused by rebuilding the child-skill list.
    private bool suppressSpecialistSkillSelectionUpdate = false;

    /// <summary>Updates the active child skill without responding to internal list refreshes.</summary>
    /// <param name="sender">The control raising the event.</param>
    /// <param name="e">The event data.</param>
    private void specialisationSkillsBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (specialisationSkillsBox.SelectedItem is TravellerSkill && !suppressSpecialistSkillSelectionUpdate)
        {
            CurrentSpecialisationSkill = (specialisationSkillsBox.SelectedItem as TravellerSkill);
            UpdateBoxes();
            specialisationSkillsBox.Focus();
        }
    }

    // Updates the active child's list label only while its parent remains selected.
    private void specialisationSkillNameBox_TextChanged(object sender, EventArgs e)
    {
        // Don't update when we're switching away from specialisation
        TravellerSkill? targetSkill = skillsBox.SelectedItem as TravellerSkill;
        if (targetSkill != null && CurrentSpecialisationSkill != null && targetSkill == CurrentSkill)
        {
            CurrentSpecialisationSkill.Name = specialisationSkillNameBox.Text;
            UpdateBoxes();
        }
    }

    // Updates the active child's summary only while its parent remains selected.
    private void specialisationSkillSummaryBox_TextChanged(object sender, EventArgs e)
    {
        // Don't update when we're switching away from specialisation
        TravellerSkill? targetSkill = skillsBox.SelectedItem as TravellerSkill;
        if (targetSkill != null && CurrentSpecialisationSkill != null && targetSkill == CurrentSkill)
        {
            CurrentSpecialisationSkill.Summary = specialisationSkillSummaryBox.Text;
        }
    }

    // Updates the active child's rules text only while its parent remains selected.
    private void specialisationSkillDescriptionBox_TextChanged(object sender, EventArgs e)
    {
        // Don't update when we're switching away from specialisation
        TravellerSkill? targetSkill = skillsBox.SelectedItem as TravellerSkill;
        if (targetSkill != null && CurrentSpecialisationSkill != null && targetSkill == CurrentSkill)
        {
            CurrentSpecialisationSkill.Description = specialisationSkillDescriptionBox.Text;
        }
    }

    // Updates the active child's referee guidance only while its parent remains selected.
    private void specialisationSkillRefereeBox_TextChanged(object sender, EventArgs e)
    {
        // Don't update when we're switching away from specialisation
        TravellerSkill? targetSkill = skillsBox.SelectedItem as TravellerSkill;
        if (targetSkill != null && CurrentSpecialisationSkill != null && targetSkill == CurrentSkill)
        {
            CurrentSpecialisationSkill.Referee = specialisationSkillRefereeBox.Text;
        }
    }
}
