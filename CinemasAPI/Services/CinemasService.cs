﻿using AutoMapper;
using CinemasAPI.Data;
using CinemasAPI.Data.Dtos.Cinema;
using CinemasAPI.Exceptions;
using CinemasAPI.Models;

namespace CinemasAPI.Services
{
    public class CinemasService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemasService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AddCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public ReadCinemaDto FetchCinema(string idCinema)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                throw new NotFoundException("Cinema não encontrado");
            }

            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

            return cinemaDto;
        }

        public void UpdateCinema(string idCinema, UpdateCinemaDto cinemaNovoDto)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                throw new NotFoundException("Cinema não encontrado");
            }

            _mapper.Map(cinemaNovoDto, cinema);

            _context.SaveChanges();
        }

        public void DeleteCinema(string idCinema)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                throw new NotFoundException("Cinema não encontrado");
            }

            _context.Remove(cinema);
            _context.SaveChanges();
        }

        private Cinema FetchCinemasPorId(string idCinema)
        {
            var cinemaQuery = from c in _context.Cinemas
                              where c.Id.ToString() == idCinema
                              select c;

            return cinemaQuery.FirstOrDefault();
        }
    }
}