using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemies
{
    public interface IEnemy
    {
        void Die();
        void TakeDamage(int damage);
    }
}
