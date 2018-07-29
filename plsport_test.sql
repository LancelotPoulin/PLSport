-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  sam. 27 jan. 2018 à 16:07
-- Version du serveur :  5.7.19
-- Version de PHP :  5.6.31

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `plsport`
--
CREATE DATABASE IF NOT EXISTS `plsport` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `plsport`;

-- --------------------------------------------------------

--
-- Structure de la table `event`
--

DROP TABLE IF EXISTS `event`;
CREATE TABLE IF NOT EXISTS `event` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(100) NOT NULL,
  `Date` datetime NOT NULL,
  `Place` int(11) NOT NULL,
  `type_ID` int(11) NOT NULL,
  `owner_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`,`type_ID`,`owner_ID`),
  KEY `fk_event_type_idx` (`type_ID`),
  KEY `fk_event_user1_idx` (`owner_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `event`
--

INSERT INTO `event` (`ID`, `Name`, `Description`, `Date`, `Place`, `type_ID`, `owner_ID`) VALUES
(1, 'Hebdo\'abdo', 'Seance abdo toutes les semaines a la salle de sport d\'Annecy', '2017-11-01 10:15:00', 10, 1, 1),
(2, 'Sortie speciale', 'Seance au paquier tranquillou', '2017-11-21 15:30:00', 15, 3, 3),
(3, 'Sortie wouhouu', 'Seance au paquier tranquillou sympa super cool wouhou', '2017-12-13 15:30:00', 5, 3, 1),
(4, 'Hebdo\'Fitness', 'Seance fitness toutes les semaines a la salle de sport d\'Ennecy', '2017-12-13 10:15:00', 3, 2, 3),
(5, 'Hebdo\'Fitness', 'Seance fitness toutes les semaines a la salle de sport d\'Ennecy', '2017-12-20 10:15:00', 8, 2, 3),
(6, 'Sortie muscu', 'Sortie special musculation', '2018-01-07 07:15:00', 9, 3, 1),
(7, 'Fitness Pro', 'Seance de 3 heures pour les plus experimente!', '2018-01-07 19:30:00', 3, 2, 3),
(8, 'Hebdo\'abdo', 'Seance abdo toutes les semaines a la salle de sport d\'Annecy', '2018-01-16 12:10:00', 0, 1, 1),
(9, 'Course', 'Course dans Annecy', '2018-01-31 08:30:00', 3, 3, 1),
(10, 'Natation', 'Natation a la piscine des marquisats', '2018-02-08 08:30:00', 0, 3, 3),
(11, 'Muscu Pro', 'Seance a la salle de sport d\'Annecy, 2 heures', '2018-03-01 15:00:00', 13, 1, 1),
(21, 'Hebdo\'abdo', 'Seance abdo toutes les semaines a la salle de sport d\'Annecy', '2018-01-10 10:15:00', 10, 1, 1),
(22, 'Sortie speciale', 'Seance au paquier tranquillou', '2018-01-31 15:30:00', 14, 3, 3),
(23, 'Hebdo\'Fitness', 'Seance fitness toutes les semaines a la salle de sport d\'Ennecy', '2018-02-21 10:15:00', 3, 2, 3),
(24, 'Sortie muscu', 'Sortie special musculation', '2018-02-27 07:15:00', 9, 3, 1),
(25, 'Fitness Pro', 'Seance de 3 heures pour les plus experimente!', '2018-02-18 19:30:00', 7, 2, 3),
(26, 'Course', 'Course dans Annecy', '2018-02-21 08:30:00', 9, 3, 1),
(27, 'Natation', 'Natation a la piscine des marquisats', '2018-03-08 08:30:00', 9, 3, 3),
(28, 'Muscu Pro', 'Seance a la salle de sport d\'Annecy, 2 heures', '2018-02-21 15:00:00', 0, 1, 1);

-- --------------------------------------------------------

--
-- Structure de la table `status`
--

DROP TABLE IF EXISTS `status`;
CREATE TABLE IF NOT EXISTS `status` (
  `ID` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `status`
--

INSERT INTO `status` (`ID`, `Name`) VALUES
(1, 'Adherent'),
(2, 'Coach');

-- --------------------------------------------------------

--
-- Structure de la table `type`
--

DROP TABLE IF EXISTS `type`;
CREATE TABLE IF NOT EXISTS `type` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `type`
--

INSERT INTO `type` (`ID`, `Name`) VALUES
(1, 'Musculation'),
(2, 'Fitness'),
(3, 'Exterieur');

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Mail` varchar(100) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Surname` varchar(50) NOT NULL,
  `Gender` varchar(10) NOT NULL,
  `Birthday` date NOT NULL,
  `Height` int(11) NOT NULL,
  `status_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`,`status_ID`),
  KEY `fk_user_status1_idx` (`status_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `user`
--

INSERT INTO `user` (`ID`, `Mail`, `Password`, `Name`, `Surname`, `Gender`, `Birthday`, `Height`, `status_ID`) VALUES
(1, 'lancelot@poulin.fr', 'lancelot', 'Poulin', 'Lancelot', 'Homme', '1998-05-26', 182, 2),
(2, 'quentin@mermaz.fr', 'quentin', 'Mermaz', 'Quentin', 'Homme', '1998-01-24', 176, 1),
(3, 'alice@ponnelle.fr', 'alice', 'Ponnelle', 'Alice', 'Femme', '1968-05-22', 170, 2),
(4, 'thomas@dasilva.fr', 'thomas', 'Da Silva', 'Thomas', 'Homme', '1998-08-18', 187, 1);

-- --------------------------------------------------------

--
-- Structure de la table `user_has_event`
--

DROP TABLE IF EXISTS `user_has_event`;
CREATE TABLE IF NOT EXISTS `user_has_event` (
  `user_ID` int(11) NOT NULL,
  `event_ID` int(11) NOT NULL,
  PRIMARY KEY (`user_ID`,`event_ID`),
  KEY `fk_user_has_event_event1_idx` (`event_ID`),
  KEY `fk_user_has_event_user1_idx` (`user_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `user_has_event`
--

INSERT INTO `user_has_event` (`user_ID`, `event_ID`) VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 2),
(1, 3),
(2, 3),
(3, 4),
(4, 4),
(2, 5),
(3, 5),
(1, 6),
(4, 6),
(2, 7),
(3, 7),
(1, 8),
(4, 8),
(1, 9),
(2, 9),
(3, 10),
(4, 10),
(1, 11),
(2, 11),
(1, 21),
(4, 21),
(2, 22),
(3, 22),
(3, 23),
(4, 23),
(1, 24),
(2, 24),
(3, 25),
(4, 25),
(1, 26),
(2, 26),
(3, 27),
(4, 27),
(1, 28);

-- --------------------------------------------------------

--
-- Structure de la table `weight`
--

DROP TABLE IF EXISTS `weight`;
CREATE TABLE IF NOT EXISTS `weight` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` date NOT NULL,
  `Value` int(11) NOT NULL,
  `user_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`,`user_ID`),
  KEY `fk_weight_user1_idx` (`user_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `weight`
--

INSERT INTO `weight` (`ID`, `Date`, `Value`, `user_ID`) VALUES
(1, '2018-01-18', 64, 1),
(2, '2018-01-12', 65, 1),
(3, '2018-01-08', 60, 1),
(4, '2018-01-07', 70, 1),
(8, '2018-01-22', 90, 1),
(9, '2018-01-22', 100, 2),
(10, '2018-01-24', 90, 2),
(12, '2018-01-24', 70, 2),
(13, '2018-01-24', 80, 2),
(14, '2018-01-24', 80, 3),
(15, '2018-01-25', 70, 3),
(16, '2018-01-25', 70, 3),
(17, '2018-01-25', 67, 2),
(18, '2018-01-25', 90, 2),
(25, '2018-01-25', 76, 2),
(26, '2018-01-25', 58, 4),
(27, '2018-01-25', 90, 4);

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `event`
--
ALTER TABLE `event`
  ADD CONSTRAINT `fk_event_type` FOREIGN KEY (`type_ID`) REFERENCES `type` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_event_user1` FOREIGN KEY (`owner_ID`) REFERENCES `user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `fk_user_status1` FOREIGN KEY (`status_ID`) REFERENCES `status` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `user_has_event`
--
ALTER TABLE `user_has_event`
  ADD CONSTRAINT `fk_user_has_event_event1` FOREIGN KEY (`event_ID`) REFERENCES `event` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_user_has_event_user1` FOREIGN KEY (`user_ID`) REFERENCES `user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `weight`
--
ALTER TABLE `weight`
  ADD CONSTRAINT `fk_weight_user1` FOREIGN KEY (`user_ID`) REFERENCES `user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
