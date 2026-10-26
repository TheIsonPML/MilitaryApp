using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryApp.Core
{
    /// <summary>
    /// Класс бизнес-логики для управления списком военнослужащих.
    /// </summary>
    public class Logic
    {
        private List<Soldier> _soldiers = new List<Soldier>();
        private int _nextId = 1;

        public Logic()
        {
            // Тестовые данные для запуска
            CreateSoldier(new Soldier { FullName = "Иванов И.И.", Rank = "Рядовой", Position = "Стрелок" });
            CreateSoldier(new Soldier { FullName = "Петров П.П.", Rank = "Сержант", Position = "Командир отделения" });
            CreateSoldier(new Soldier { FullName = "Сидоров С.С.", Rank = "Лейтенант", Position = "Командир взвода" });
        }

        /// <summary>Создает и добавляет нового военнослужащего.</summary>
        public void CreateSoldier(Soldier soldier)
        {
            soldier.Id = _nextId++;
            _soldiers.Add(soldier);
        }

        /// <summary>Возвращает список всех военнослужащих.</summary>
        public List<Soldier> ReadAllSoldiers()
        {
            return _soldiers.ToList();
        }

        /// <summary>Обновляет данные военнослужащего.</summary>
        public void UpdateSoldier(Soldier updatedSoldier)
        {
            var existing = _soldiers.FirstOrDefault(s => s.Id == updatedSoldier.Id);
            if (existing != null)
            {
                existing.FullName = updatedSoldier.FullName;
                existing.Rank = updatedSoldier.Rank;
                existing.Position = updatedSoldier.Position;
            }
        }

        /// <summary>Удаляет военнослужащего по ID.</summary>
        public void DeleteSoldier(int id)
        {
            _soldiers.RemoveAll(s => s.Id == id);
        }

        /// <summary>Бизнес-функция 1: Поиск по званию.</summary>
        public List<Soldier> GetSoldiersByRank(string rank)
        {
            return _soldiers.Where(s => s.Rank.ToLower().Contains(rank.ToLower())).ToList();
        }

        /// <summary>Бизнес-функция 2: Группировка по должности.</summary>
        public Dictionary<string, List<Soldier>> GroupSoldiersByPosition()
        {
            return _soldiers.GroupBy(s => s.Position)
                            .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}