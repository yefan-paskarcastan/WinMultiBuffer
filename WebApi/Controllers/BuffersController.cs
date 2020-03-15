﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MultiBuffer.WebApi.DataModels;
using MultiBuffer.IWebApi;
using MultiBuffer.WebApi.Utils;

namespace MultiBuffer.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BuffersController : ControllerBase
    {
        /// <summary>
        /// Добавляет экземплят буфера в базу.
        /// </summary>
        /// <param name="bufferItem">Экземпляр буфера</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(WebBuffer bufferItem)
        {
            var contextDb = new MultiBufferContext();
            var query =
                from el in contextDb.BufferItems
                where el.Key == bufferItem.Key
                select el;

            BufferItem item = query.SingleOrDefault();
            if (item == null)
            {
                try
                {
                    item = new BufferItem()
                    {
                        Name = bufferItem.Name,
                        Value = bufferItem.Value,
                        Key = bufferItem.Key,
                    };

                    contextDb.BufferItems.Add(item);
                    contextDb.SaveChanges();
                }
                catch (Exception)
                {
                    return RequestResult.ServerError;
                }
                return RequestResult.Success;
            }
            return RequestResult.ClientError;
        }

        /// <summary>
        /// Возвращает экземляр буфера
        /// </summary>
        /// <param name="keyNumber">Номер привязанной клавиши</param>
        /// <returns></returns>
        [HttpGet("{keyNumber}")]
        public WebBuffer Read(int keyNumber)
        {
            var context = new MultiBufferContext();

            var query =
                from el in context.BufferItems
                where el.Key == keyNumber
                select el;
            BufferItem item = query.SingleOrDefault();

            var itemWebApi = new WebBuffer();
            if (item == null) return itemWebApi;

            itemWebApi.Key = item.Key;
            itemWebApi.Name = item.Name;
            itemWebApi.Value = item.Value;
            return itemWebApi;
        }

        /// <summary>
        /// Обновляет экземпляр указанного буфера
        /// </summary>
        /// <param name="keyNumber">Клавиша, привязанная к буферу</param>
        /// <param name="bufferItem">Экземпляр буфера с обновленными данными</param>
        /// <returns></returns>
        [HttpPut("{keyNumber}")]
        public IActionResult Update(int keyNumber, WebBuffer bufferItem)
        {
            if(keyNumber != bufferItem.Key) return RequestResult.ClientError;

            var context = new MultiBufferContext();

            var query =
                from el in context.BufferItems
                where el.Key == keyNumber
                select el;

            BufferItem item = query.SingleOrDefault();

            if (item == null) return RequestResult.ClientError;

            try
            {
                item.Value = bufferItem.Value;
                context.SaveChanges();
            }
            catch (Exception)
            {
                return RequestResult.ServerError;
            }
            return RequestResult.Success;
        }

        /// <summary>
        /// Удалить указанный буфер
        /// </summary>
        /// <param name="keyNumber">Клавиша, привязнная к буферу</param>
        /// <returns></returns>
        [HttpDelete("{keyNumber}")]
        public IActionResult Delete(int keyNumber)
        {
            var context = new MultiBufferContext();

            var query =
                from el in context.BufferItems
                where el.Key == keyNumber
                select el;

            BufferItem item = query.SingleOrDefault();

            if (item == null) { return RequestResult.ClientError; }

            try
            {
                context.BufferItems.Remove(item);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return RequestResult.ServerError;
            }
            return RequestResult.Success;
        }
    }
}