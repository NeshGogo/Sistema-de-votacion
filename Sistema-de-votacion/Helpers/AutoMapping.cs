using AutoMapper;
using Sistema_de_votacion.Models;
using Sistema_de_votacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Helpers
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<ElectionCreateViewModel, Election>()
                .ForMember(e => e.ElectionCadidate, c => c.Ignore())
                .ForMember(e => e.ElectionCitizen, ev => ev.Ignore())
                .ForMember(e => e.ElectionPoliticParty, ev => ev.Ignore())
                .ForMember(e => e.ElectionPosition, ev => ev.Ignore());
            CreateMap<Candidate, CandidateElectionViewModel>();
        }
    }
}
