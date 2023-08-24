using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_CSharp
{

    internal struct recouses
    {
        int budget;
        int materials;
        int provision;
        int medicine;
        int permissions;
    }

    enum buildings
    {
        noBuilding = 0,
        bank = 1,
        factory = 2,
        grocery = 3,
        pharmacy = 4

    }

    internal abstract class mapArea
    {
        public string name;
        public string description;

        public mapArea(string name, string description)
        {
            this.name = name;
            this.description = description;

        }

    }

    internal class emptyArea : mapArea
    {
        buildings building;

        public emptyArea(string name = "Пустырь", string description =
            "Пустырь, расчищенный когда-то рабочими. Здесь никто не живёт, но это место можно застроить.")
            : base(name, description)
        {
            building = buildings.noBuilding;
        }
    }

    internal abstract class usualArea : mapArea
    {
        protected int requiredBudget;
        protected int requiredMaterials;
        protected int requiredProvision;
        protected int requiredMedicine;

        protected int currentBudget;
        protected int currentMaterials;
        protected int currentProvision;
        protected int currentMedicine;

        public usualArea(string name, int requiredBudget, int requiredMaterials, int requiredProvision, int requiredMedicine, string description) : base(name, description)
        {
            this.requiredBudget = requiredBudget;
            this.requiredMaterials = requiredMaterials;
            this.requiredProvision = requiredProvision;
            this.requiredMedicine = requiredMedicine;

            currentBudget = 0;
            currentMaterials = 0;
            currentProvision = 0;
            currentMedicine = 0;
        }
    }

    internal class blokpost : usualArea
    {
        string status;
        public blokpost(string name = "Блокпост", string status = "Неизвестен", int requiredBudget = 0, int requiredMaterials = 0, int requiredProvision = 0, int requiredMedicine = 0, string description =
            "Армейский блокпост. Охраняет город и ведёт патруль местности. Каждый блокпост сразу свяжется с ратушей в случае наблюдения подозрительных или опасных вещей.")
            : base(name, requiredBudget, requiredMaterials, requiredProvision, requiredMedicine, description)
        {
            this.status = status;
        }
    }
    internal class townArea : usualArea
    {
        int spirits;
        public townArea(string name = "Жилой район", int spirits = 70, int requiredBudget = 0, int requiredMaterials = 0, int requiredProvision = 0, int requiredMedicine = 0, string description =
            "Этот район не играет ключевой роли в жизни города: люди возвращаются сюда лишь для того, чтобы переночевать, а утром вновь отправиться на работу.")
            : base(name, requiredBudget, requiredMaterials, requiredProvision, requiredMedicine, description)
        {
            {
                this.spirits = spirits;
            }
        }
    }
}
