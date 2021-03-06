﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Data.Entity;


namespace AutoCourse.DAL
{
    /// <summary>
    /// 基本的数据库操作
    /// </summary>
    public class ACBaseDBOption<T> where T : class
    {
        AutoCourse.Models.AutoCourseEntities ACEntities = ACDBContext.GetCurrDBContext();

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">增加的记录</param>
        /// <param name="IsSave">是否保存</param>
        /// <returns>返回增加的记录</returns>
        public T Add(T entity, bool IsSave = true)
        {
            ACEntities.Set<T>().Add(entity);
            if (IsSave)
            {
                ACEntities.SaveChanges();
            }
            return entity;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">更新的记录</param>
        /// <param name="IsSave">是否保存</param>
        /// <returns>返回更新的记录</returns>
        public T UpDate(T entity, bool IsSave = true)
        {
            ACEntities.Set<T>().Attach(entity);
            ACEntities.Entry<T>(entity).State = EntityState.Modified;

            if (IsSave)
            {
                ACEntities.SaveChanges();

            }
            return entity;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">需要删除的记录</param>
        /// <param name="IsSave">是否保存</param>
        public bool Delete(T entity, bool IsSave = true)
        {
            ACEntities.Set<T>().Attach(entity);
            ACEntities.Set<T>().Remove(entity);
            if (IsSave)
            {
                return ACEntities.SaveChanges() > 0;
            }
            else
                return true;
        }

        /// <summary>
        /// 根据主键查找一条记录，请注意按照主键的顺序
        /// </summary>
        /// <param name="keyvalues">可以传入多个参数，不限制类型</param>
        /// <returns>T</returns>
        public T Find(params object[] keyvalues)
        {
            return ACEntities.Set<T>().Find(keyvalues);
        }

        /// <summary>
        /// 根据条件查询一条记录
        /// </summary>
        /// <param name="lambda">Lambda表达式</param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> lambda)
        {
            return ACEntities.Set<T>().FirstOrDefault(lambda);
        }

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <param name="number">返回的记录数【0-返回所有】</param>
        /// <param name="whereLandba">查询条件</param>
        /// <param name="orderType">排序方式</param>
        /// <param name="orderLandba">排序条件</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> whereLandba)
        {
            return ACEntities.Set<T>().Where(whereLandba);
        }

        public int Save()
        {
            return ACEntities.SaveChanges();
        }

    }
}