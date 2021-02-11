using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.WebUI.Models
{
    public class TournamentViewModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string LogoPath { get; set; }
        public int Id { get; set; }
        public SelectList GamesSelectList { get; set; }
    }
}