namespace ModernMUDEditor
{
    partial class EditSpells
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstSpells = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbSpells = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstSpells
            // 
            this.lstSpells.FormattingEnabled = true;
            this.lstSpells.Location = new System.Drawing.Point(13, 13);
            this.lstSpells.Name = "lstSpells";
            this.lstSpells.Size = new System.Drawing.Size(259, 95);
            this.lstSpells.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(70, 141);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(151, 141);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(70, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(151, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbSpells
            // 
            this.cbSpells.FormattingEnabled = true;
            this.cbSpells.Items.AddRange(new object[] {
            "acid blast",
            "acid breath",
            "acid spray",
            "adaptation",
            "adrenaline control",
            "agitation",
            "agony",
            "aid",
            "airy smith",
            "airy starshell",
            "airy water",
            "analyze balance",
            "animate dead",
            "apocalypse",
            "aquatic smith",
            "arieks shattering iceball",
            "armor",
            "aura",
            "aura sight",
            "awe",
            "azure flame",
            "ball lightning",
            "ballistic attack",
            "barkskin",
            "battering stonestorm",
            "battle ecstasy",
            "bearstrength",
            "bigbys clenched fist",
            "biofeedback",
            "bird of prey",
            "blending",
            "bless",
            "blindness",
            "blur",
            "bombard",
            "bonethunder",
            "burning hands",
            "call lightning",
            "call of the wild",
            "cause critical",
            "cause light",
            "cause serious",
            "celestial sword",
            "cell adjustment",
            "chain lightning",
            "chameleons cantrip",
            "chameleons charm",
            "change self",
            "change sex",
            "charm person",
            "chill of the windsaber",
            "chill touch",
            "cloak of fear",
            "clone",
            "cold ward",
            "coldshield",
            "color spray",
            "combat mind",
            "complete healing",
            "comprehend languages",
            "concealment",
            "cone of cold",
            "cone of silence",
            "conflaguration",
            "conjure windsabre",
            "continual light",
            "control _weather",
            "control flames",
            "cowardice",
            "create buffet",
            "create dracolich",
            "create food",
            "create skin",
            "create sound",
            "create spring",
            "create water",
            "creeping doom",
            "cure blindness",
            "cure critical",
            "cure disease",
            "cure light",
            "cure serious",
            "curse",
            "cutting breeze",
            "cyclone",
            "darkness",
            "dazzle",
            "deathfield",
            "demi shadow magic",
            "demi shadow monsters",
            "deny air",
            "deny earth",
            "deny fire",
            "deny water",
            "destroy cursed",
            "destroy undead",
            "detect evil",
            "detect good",
            "detect invis",
            "detect magic",
            "detect poison",
            "detect undead",
            "detonate",
            "dexterity",
            "dimension door",
            "dirt cloud",
            "disintegrate",
            "dismissal",
            "dispel evil",
            "dispel good",
            "dispel invisible",
            "dispel magic",
            "displacement",
            "domination",
            "dust blast",
            "earthen grasp",
            "earthen rain",
            "earthen smith",
            "earthen starshell",
            "earthen tomb",
            "earthquake",
            "ectoplasmic form",
            "ego whip",
            "elemental creation",
            "elemental form",
            "elemental sight",
            "elephantstrength",
            "enchant weapon",
            "energy containment",
            "energy drain",
            "enhance armor",
            "enhanced strength",
            "enlarge",
            "entangle",
            "enthrall",
            "ethereal shield",
            "etherportal",
            "exorcise",
            "faerie fire",
            "faerie fog",
            "farsee",
            "fear",
            "feeblemind",
            "fiery smith",
            "fiery starshell",
            "fire bolt",
            "fire breath",
            "fire ward",
            "fireball",
            "fireshield",
            "fireskin",
            "firestorm",
            "flameburst",
            "flamestrike",
            "flashfire",
            "flesh armor",
            "flickering flame",
            "flight",
            "fly",
            "frost breath",
            "full harm",
            "full heal",
            "gas breath",
            "gate",
            "general purpose",
            "gleam of dawn",
            "gleam of dusk",
            "greater agony",
            "greater earthen grasp",
            "greater mending",
            "greater pythonsting",
            "greater ravenflight",
            "greater soul disturbance",
            "greater spirit sight",
            "greater spirit ward",
            "greater stamina",
            "greater sustenance",
            "group globe",
            "group heal",
            "group recall",
            "group stoneskin",
            "gust of frost",
            "harbor of balance",
            "harm",
            "haste",
            "hawkvision",
            "heal",
            "heal undead",
            "herbal remedy",
            "hex",
            "high explosive",
            "holy sacrifice",
            "holy word",
            "hover",
            "hurricane",
            "hypnotic pattern",
            "ice bolt",
            "ice missile",
            "ice storm",
            "identify",
            "illumination",
            "illusion of incompetence",
            "illusion of prowess",
            "illusionary wall",
            "immolate",
            "incendiary cloud",
            "inertial barrier",
            "inferno",
            "inflict pain",
            "infravision",
            "intellect fortress",
            "invisibility",
            "judgement",
            "know alignment",
            "lend health",
            "lesser herbal remedy",
            "lesser mending",
            "lesser nourishment",
            "levitation",
            "lightanddark",
            "lightning bolt",
            "lightning breath",
            "lightning curtain",
            "lionrage",
            "locate object",
            "magic missile",
            "magnetism",
            "major globe",
            "malison",
            "mana drain",
            "mass dispel magic",
            "mass heal",
            "mass invisibility",
            "mass vortex lift",
            "melfs acid arrow",
            "mending",
            "mental barrier",
            "mermaids kiss",
            "meteor swarm",
            "migration",
            "mind thrust",
            "miners intuition",
            "minor agony",
            "minor blending",
            "minor creation",
            "minor globe",
            "minor paralysis",
            "mirage arcana",
            "mirror image",
            "misdirection",
            "molevision",
            "molten spray",
            "moonwell",
            "mousestrength",
            "negate hex",
            "negate luster",
            "negate veil",
            "neural fragmentation",
            "nightmares",
            "nourishment",
            "pantherspeed",
            "pass door",
            "pebble",
            "phantasmal killer",
            "plague",
            "plane shift",
            "poison",
            "polymorph other",
            "power word blind",
            "power word lag",
            "power word stun",
            "preserve",
            "prismatic orb",
            "prismatic spray",
            "project force",
            "protect undead",
            "protection from acid",
            "protection from cold",
            "protection from evil",
            "protection from fire",
            "protection from gas",
            "protection from good",
            "protection from lightning",
            "psionic blast",
            "psychic crush",
            "psychic healing",
            "purify",
            "purify spirit",
            "pythonsting",
            "rain maker",
            "raise lich",
            "raise skeleton",
            "raise spectre",
            "raise vampire",
            "raise wraith",
            "raise zombie",
            "ravenflight",
            "ravenous vines",
            "recharge item",
            "reduce",
            "rejuvenate",
            "relocate",
            "remove alignment",
            "remove curse",
            "remove poison",
            "remove silence",
            "reserved",
            "resurrect",
            "reveal spirit essence",
            "reveal true form",
            "reveal true Name",
            "ripple",
            "sanctuary",
            "scalding blast",
            "scathing wind",
            "scorching touch",
            "sense life",
            "sense spirit",
            "shades",
            "shadow door",
            "shadow magic",
            "shadow monsters",
            "shadow shield",
            "shadow veil",
            "shadow walk",
            "share strength",
            "shield",
            "shocking grasp",
            "shockshield",
            "shrewtameness",
            "silence",
            "sleep",
            "slow poison",
            "slowness",
            "snailspeed",
            "soothe wound",
            "soul disturbance",
            "soulshield",
            "spark",
            "spirit anguish",
            "spirit armor",
            "spirit jump",
            "spirit sight",
            "spirit ward",
            "spook",
            "stamina",
            "stoneskin",
            "stonestrength",
            "stornogs spheres",
            "strength",
            "summon",
            "summon elemental",
            "summon fire serpent",
            "summon greater elemental",
            "summon nerneid",
            "summon pech",
            "summon slyph",
            "sunburst",
            "sunray",
            "sustenance",
            "telekinesis",
            "teleport",
            "thirst",
            "thought shield",
            "tidal wave",
            "tide of the seas",
            "touch of nightshade",
            "transfer wellness",
            "trap-acid",
            "trap-bash",
            "trap-cold",
            "trap-disease",
            "trap-dispel",
            "trap-energy",
            "trap-fire",
            "trap-gate",
            "trap-harm",
            "trap-para",
            "trap-pierce",
            "trap-poison",
            "trap-slash",
            "trap-sleep",
            "trap-stun",
            "trap-summon",
            "trap-teleport",
            "trap-wither",
            "tridenttides",
            "true seeing",
            "turn undead",
            "twilight",
            "ultra death ray",
            "ultrablast",
            "unholy word",
            "vacancy",
            "ventriloquate",
            "vigorize critical",
            "vigorize light",
            "vigorize serious",
            "vitality",
            "wall of fire",
            "wall of force",
            "wall of ice",
            "wall of iron",
            "wall of mist",
            "wall of sparks",
            "wall of stone",
            "water blast",
            "water bolt",
            "water breath",
            "water breathing",
            "watery starshell",
            "weaken",
            "weird",
            "wellness",
            "whirlwind",
            "wither",
            "wizard eye",
            "wolfspeed",
            "word of recall",
            "word of thendar",
            "wormhole"});
            this.cbSpells.Location = new System.Drawing.Point(46, 114);
            this.cbSpells.Name = "cbSpells";
            this.cbSpells.Size = new System.Drawing.Size(226, 21);
            this.cbSpells.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Spell";
            // 
            // EditSpells
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 204);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSpells);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstSpells);
            this.MaximizeBox = false;
            this.Name = "EditSpells";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Spells";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstSpells;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbSpells;
        private System.Windows.Forms.Label label1;
    }
}