﻿using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Attachment
{
    /// <summary>
    /// Вложение.
    /// </summary>
    public class AttachmentDto : BaseDto
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Содержимое в двоичном виде.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
