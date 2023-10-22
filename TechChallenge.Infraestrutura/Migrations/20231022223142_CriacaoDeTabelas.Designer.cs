﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechChallenge.Infraestrutura.Data;

#nullable disable

namespace TechChallenge.Infraestrutura.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231022223142_CriacaoDeTabelas")]
    partial class CriacaoDeTabelas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("App")
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Solucionadores", b =>
                {
                    b.Property<int>("AtividadesId")
                        .HasColumnType("int");

                    b.Property<int>("SolucionadoresId")
                        .HasColumnType("int");

                    b.HasKey("AtividadesId", "SolucionadoresId");

                    b.HasIndex("SolucionadoresId");

                    b.ToTable("Solucionadores", "App");
                });

            modelBuilder.Entity("TechChallenge.Dominio.Atividade.Atividade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartamentoResponsavel")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EstahAtiva")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrazoEstimado")
                        .HasColumnType("int");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.Property<int>("TipoDeDistribuicao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Atividades", "App");
                });

            modelBuilder.Entity("TechChallenge.Dominio.Demanda.Demanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AtividadeId")
                        .HasColumnType("int");

                    b.Property<int>("DepartamentoResponsavel")
                        .HasColumnType("int");

                    b.Property<int>("DepartamentoSolicitante")
                        .HasColumnType("int");

                    b.Property<string>("Detalhes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdDaDemandaReaberta")
                        .HasColumnType("int");

                    b.Property<DateTime>("MomentoDeAbertura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MomentoDeFechamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Prazo")
                        .HasColumnType("datetime2");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioResponsavelId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioSolicitanteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AtividadeId");

                    b.HasIndex("UsuarioResponsavelId");

                    b.HasIndex("UsuarioSolicitanteId");

                    b.ToTable("Demandas", "App");
                });

            modelBuilder.Entity("TechChallenge.Dominio.EventoRegistrado.EventoRegistrado", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("DemandaId")
                        .HasColumnType("int");

                    b.Property<int>("DepartamentoResponsavel")
                        .HasColumnType("int");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MomentoFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MomentoInicial")
                        .HasColumnType("datetime2");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioResponsavelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DemandaId");

                    b.HasIndex("UsuarioResponsavelId");

                    b.ToTable("EventosRegistrados", "App");
                });

            modelBuilder.Entity("TechChallenge.Dominio.Usuario.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Departamento")
                        .HasColumnType("int");

                    b.Property<int>("Funcao")
                        .HasColumnType("int");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", "App");
                });

            modelBuilder.Entity("Solucionadores", b =>
                {
                    b.HasOne("TechChallenge.Dominio.Atividade.Atividade", null)
                        .WithMany()
                        .HasForeignKey("AtividadesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechChallenge.Dominio.Usuario.Usuario", null)
                        .WithMany()
                        .HasForeignKey("SolucionadoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TechChallenge.Dominio.Demanda.Demanda", b =>
                {
                    b.HasOne("TechChallenge.Dominio.Atividade.Atividade", "Atividade")
                        .WithMany("Demandas")
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechChallenge.Dominio.Usuario.Usuario", "UsuarioResponsavel")
                        .WithMany()
                        .HasForeignKey("UsuarioResponsavelId");

                    b.HasOne("TechChallenge.Dominio.Usuario.Usuario", "UsuarioSolicitante")
                        .WithMany()
                        .HasForeignKey("UsuarioSolicitanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atividade");

                    b.Navigation("UsuarioResponsavel");

                    b.Navigation("UsuarioSolicitante");
                });

            modelBuilder.Entity("TechChallenge.Dominio.EventoRegistrado.EventoRegistrado", b =>
                {
                    b.HasOne("TechChallenge.Dominio.Demanda.Demanda", "Demanda")
                        .WithMany("EventosRegistrados")
                        .HasForeignKey("DemandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechChallenge.Dominio.Usuario.Usuario", "UsuarioResponsavel")
                        .WithMany()
                        .HasForeignKey("UsuarioResponsavelId");

                    b.Navigation("Demanda");

                    b.Navigation("UsuarioResponsavel");
                });

            modelBuilder.Entity("TechChallenge.Dominio.Atividade.Atividade", b =>
                {
                    b.Navigation("Demandas");
                });

            modelBuilder.Entity("TechChallenge.Dominio.Demanda.Demanda", b =>
                {
                    b.Navigation("EventosRegistrados");
                });
#pragma warning restore 612, 618
        }
    }
}
