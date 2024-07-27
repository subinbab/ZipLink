using Models.DTO;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.URLOperations
{
    public class URLOperators : IURLOperators
    {
        private ZiplinkDBContext _ziplinkDbContext { get; set; }
        public URLOperators(ZiplinkDBContext ziplinkDbContext)
        {
            _ziplinkDbContext = ziplinkDbContext;
        }
        public List<URLDTO> Get()
        {
            try
            {
                var result = _ziplinkDbContext.urlOperator.ToList();
                return result;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public URLDTO GetById(int id)
        {
            try
            {
                var result = _ziplinkDbContext.urlOperator.Find(id);
                return result;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public URLDTO Add(URLDTO model)
        {
            model.user = null;
            try
            {
                var result = _ziplinkDbContext.urlOperator.Add(model);
                _ziplinkDbContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Remove(URLDTO model)
        {
            try
            {
                var result = _ziplinkDbContext.urlOperator.Remove(model);
                _ziplinkDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool Update(URLDTO urldto)
        {
            try
            {
                var result = _ziplinkDbContext.urlOperator.Add(urldto);
                _ziplinkDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public URLDTO GetDataByParms(string param, string data, string type)
        {
            try
            {
                return _ziplinkDbContext.urlOperator.Where(c=>c.generatedUrl.Equals(data)).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       /* public URLDTO GetDataByParms(string param , string data , string type)
        {
            try
            {
                // Get the IQueryable for the entity type
                IQueryable<URLDTO> query = _ziplinkDbContext.Set<URLDTO>();

                // Create a parameter expression for the entity type
                ParameterExpression parameter = Expression.Parameter(typeof(URLDTO), "class");

                // Create a member access expression for the property
                MemberExpression property = Expression.Property(parameter, param);

                // Create a constant expression for the value
                ConstantExpression constant = Expression.Constant(data);

                // Create a binary expression for the equality comparison
                BinaryExpression equality = Expression.Equal(property, constant);

                // Create a lambda expression for the predicate
                Expression<Func<URLDTO, bool>> predicate = Expression.Lambda<Func<URLDTO, bool>>(equality, parameter);

                // Apply the Where condition using the predicate
               var result =  query.Where(predicate);
                return result.ToList().First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }*/
    }
}
