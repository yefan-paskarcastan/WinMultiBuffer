﻿using System;
using System.Windows.Forms;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class InputControllerEventArgs : EventArgs
    {
        /// <summary>
        /// Передает нажатаю клавишу и значение, если это копирование
        /// </summary>
        /// <param name="key">Нажатя клавшиа</param>
        /// <param name="pasteKey">Значение буфера</param>
        public InputControllerEventArgs(Keys key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Нажатая клавиша
        /// </summary>
        public readonly Keys Key;

        /// <summary>
        /// Значение буфера обмена
        /// </summary>
        public readonly string Value;
    }
}
