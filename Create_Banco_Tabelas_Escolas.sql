
CREATE DATABASE db_escola
GO
USE db_escola
GO
CREATE TABLE aluno (
                id INTEGER PRIMARY KEY NOT NULL,
                nome VARCHAR(255),
                usuario VARCHAR(45),
				senha VARCHAR(60));

CREATE TABLE turma (
                id INTEGER PRIMARY KEY NOT NULL,
                curso_id INTEGER,
                turma VARCHAR(45),
                ano SMALLINT);

CREATE TABLE alunoturma (
                alunoturma_id INTEGER PRIMARY KEY NOT NULL,
                aluno_id INTEGER,
                turma_id INTEGER,
                CONSTRAINT fk_aluno_id FOREIGN KEY (aluno_id) REFERENCES aluno (id),
                CONSTRAINT fk_turma_id FOREIGN KEY (turma_id) REFERENCES turma (id)
	            ON DELETE CASCADE ON UPDATE CASCADE);


