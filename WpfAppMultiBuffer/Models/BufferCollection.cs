﻿using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace WpfAppMultiBuffer.Models
{
    class BufferCollection : ObservableCollection<BufferItem>
    {
        /// <summary>
        /// Добавляет новое содержимое в буфер
        /// </summary>
        /// <param name="refKey">Ссылочный ключ, предоставляет доступ к значимому ключу</param>
        /// <param name="valueKey">Значимый ключ, предоставляет доступ к значению</param>
        /// <param name="value">Значение</param>
        public void Add(Keys refKey, Keys valueKey, string value)
        {
            if (refKey == valueKey)
                throw new Exception("Ref key and value key must be unique.");

            BufferItem item = new BufferItem
            {
                Index = base.Count,
                RefKey = refKey,
                ValueKey = valueKey,
                Value = value,
            };

            base.Add(item);
        }
        /// <summary>
        /// Добавляет массив элементов в буфер
        /// </summary>
        /// <param name="refKeys">Массив ссылоных ключей</param>
        /// <param name="valueKeys">Массив значимых ключей, длина должна быть равна массиву ссылочных ключей</param>
        /// <param name="value">Значение по умолчанию</param>
        public void AddRange(Keys[] refKeys, Keys[] valueKeys, string value)
        {
            if (refKeys.Length != valueKeys.Length)
                throw new Exception("Count key must be same.");

            for (int i = 0; i < refKeys.Length; i++)
            {
                Add(refKeys[i], valueKeys[i], value);
            }
        }
        /// <summary>
        /// Получить или установить значение буфера по клавише
        /// </summary>
        /// <param name="inputKey">Нажатая клавиша</param>
        /// <returns>Возвращает значение буфера</returns>
        public string this[Keys inputKey]
        {
            get
            {
                string value;

                try
                {
                    value =
                        (from el in this
                         where el.RefKey == inputKey || el.ValueKey == inputKey
                         select el.Value).Single();
                }
                catch (ArgumentNullException e)
                {
                    throw new ArgumentNullException("Buffer collection is null", e);
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException("Did not find input key in buffer collection", e);
                }

                return value;
            }
            set
            {
                BufferItem item;

                try
                {
                    item =
                        (from el in this
                         where el.RefKey == inputKey || el.ValueKey == inputKey
                         select el).Single();
                }
                catch (ArgumentNullException e)
                {
                    throw new ArgumentNullException("Buffer collection is null", e);
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException("Did not find input key in buffer collection", e);
                }

                item.Value = value;
                base.SetItem(item.Index, item);
            }
        }
    }
}