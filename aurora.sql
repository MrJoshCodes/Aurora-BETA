-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 17, 2017 at 03:49 AM
-- Server version: 10.1.25-MariaDB
-- PHP Version: 5.6.31

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `aurora`
--

-- --------------------------------------------------------

--
-- Table structure for table `catalog_deals`
--

CREATE TABLE `catalog_deals` (
  `id` int(11) NOT NULL,
  `template_id` int(11) NOT NULL,
  `amount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `catalog_deals`
--

INSERT INTO `catalog_deals` (`id`, `template_id`, `amount`) VALUES
(1, 1, 1),
(1, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `catalog_pages`
--

CREATE TABLE `catalog_pages` (
  `id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL,
  `icon_color` int(11) NOT NULL,
  `icon_image` int(11) NOT NULL,
  `in_development` tinyint(1) NOT NULL,
  `is_visible` tinyint(1) NOT NULL,
  `parent_id` int(11) NOT NULL,
  `min_rank` int(11) NOT NULL,
  `layout` varchar(15) NOT NULL,
  `has_content` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `catalog_pages`
--

INSERT INTO `catalog_pages` (`id`, `name`, `icon_color`, `icon_image`, `in_development`, `is_visible`, `parent_id`, `min_rank`, `layout`, `has_content`) VALUES
(1, 'Frontpage', 1, 1, 0, 1, 0, 1, 'frontpage3', 1),
(2, 'Furniture Shop', 3, 64, 0, 1, 0, 1, '', 0),
(3, 'Some furni', 8, 10, 0, 1, 2, 1, 'default_3x3', 1),
(4, 'Trophies', 0, 60, 0, 1, 2, 1, 'trophies', 1);

-- --------------------------------------------------------

--
-- Table structure for table `catalog_pages_data`
--

CREATE TABLE `catalog_pages_data` (
  `id` int(11) NOT NULL,
  `type` varchar(5) NOT NULL,
  `value` text NOT NULL,
  `page_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `catalog_pages_data`
--

INSERT INTO `catalog_pages_data` (`id`, `type`, `value`, `page_id`) VALUES
(1, 'image', 'catalog_frontpage_headline2_en', 1),
(2, 'image', 'topstory_habbo_beta', 1),
(3, 'image', 'frontpage_sms4', 1),
(4, 'text', 'Catalog in BETA', 1),
(5, 'text', 'More furniture coming soon...', 1),
(6, 'text', '', 1),
(7, 'text', 'What is the currency?', 1),
(8, 'text', 'In Aurora, you can buy normal furniture with pixels and credits. Special furniture can be bought with special points. You can purchase these points with real money or earn/win them.', 1),
(9, 'image', 'catalog_trophies_headline1_en', 4),
(10, 'text', 'Reward your Habbo friends, or yourself with one of our fabulous glittering array of bronze, silver and gold trophies.<br><br>First choose the trophy model (click on the arrows to see all the different styles) and then the metal (click on the seal below the trophy). Type your inscription below and we\'\'ll engrave it on the trophy along with your name and today\'\'s date.<br>', 4),
(11, 'text', 't1:Type your inscription CAREFULLY, it\'\'s permanent!', 4);

-- --------------------------------------------------------

--
-- Table structure for table `catalog_products`
--

CREATE TABLE `catalog_products` (
  `id` int(11) NOT NULL,
  `name` varchar(30) NOT NULL,
  `price_coins` int(11) NOT NULL,
  `price_pixels` int(11) NOT NULL,
  `amount` int(11) NOT NULL,
  `page_id` int(11) NOT NULL,
  `template_id` int(11) DEFAULT NULL,
  `is_deal` tinyint(1) NOT NULL DEFAULT '0',
  `deal_id` int(11) DEFAULT NULL,
  `data` varchar(30) NOT NULL COMMENT 'The poster ID if it''s a poster'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `catalog_products`
--

INSERT INTO `catalog_products` (`id`, `name`, `price_coins`, `price_pixels`, `amount`, `page_id`, `template_id`, `is_deal`, `deal_id`, `data`) VALUES
(1, 'a0 throne', 25, 0, 1, 3, 1, 0, NULL, ''),
(2, 'deal_throne_typo', 35, 0, 0, 3, NULL, 1, 1, ''),
(3, 'poster 1004', 3, 0, 1, 3, 3, 0, NULL, '1004'),
(4, 'a0 prizetrophy_g', 12, 0, 1, 4, 4, 0, NULL, ''),
(5, 'a0 prizetrophy_s', 10, 0, 1, 4, 5, 0, NULL, ''),
(6, 'a0 prizetrophy_b', 8, 0, 1, 4, 6, 0, NULL, '');

-- --------------------------------------------------------

--
-- Table structure for table `catalog_vouchers`
--

CREATE TABLE `catalog_vouchers` (
  `voucher` varchar(50) NOT NULL DEFAULT '',
  `reward` int(11) NOT NULL DEFAULT '0',
  `type` enum('Pixel','Credit','Item') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `frontpage_items`
--

CREATE TABLE `frontpage_items` (
  `id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL,
  `description` text NOT NULL,
  `image` text NOT NULL,
  `size` int(11) NOT NULL,
  `type` int(11) NOT NULL,
  `tag` varchar(20) NOT NULL,
  `room_id` int(11) NOT NULL,
  `external_text` varchar(30) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `frontpage_items`
--

INSERT INTO `frontpage_items` (`id`, `name`, `description`, `image`, `size`, `type`, `tag`, `room_id`, `external_text`) VALUES
(1, 'Welcome Lounge', 'New? Lost? Get a warm welcome here!', '', 1, 3, 'events', 1, 'welcome_lounge'),
(2, 'Theatredrome', 'Join in all the fun of the fair!', '', 1, 3, 'events', 2, 'theatredrome'),
(3, 'Chinese Tea Room', 'Try the tea in this Mongol cafe - it is to die for darlings!', '', 1, 3, 'events', 3, 'tearoom');

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `id` int(11) NOT NULL,
  `room_id` int(11) DEFAULT NULL,
  `owner_id` int(11) NOT NULL,
  `definition_id` int(11) NOT NULL,
  `x` int(3) NOT NULL DEFAULT '0',
  `y` int(3) NOT NULL DEFAULT '0',
  `z` double NOT NULL DEFAULT '0',
  `rotation` int(2) NOT NULL DEFAULT '0',
  `data` text NOT NULL,
  `wallposition` varchar(40) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`id`, `room_id`, `owner_id`, `definition_id`, `x`, `y`, `z`, `rotation`, `data`, `wallposition`) VALUES
(1, 1, 1, 7, 16, 0, 0, 0, 'a016', ''),
(2, 1, 1, 8, 17, 0, 0, 4, 'y017', ''),
(3, 1, 1, 9, 18, 0, 0, 4, 'w018', ''),
(4, 1, 1, 10, 19, 0, 0, 4, 'v019', ''),
(5, 1, 1, 7, 20, 0, 0, 0, 'a020', ''),
(6, 1, 1, 11, 16, 1, 0, 2, 'b116', ''),
(7, 1, 1, 7, 7, 2, 0, 0, 'a27', ''),
(8, 1, 1, 7, 11, 2, 0, 0, 'a211', ''),
(9, 1, 1, 11, 16, 2, 0, 2, 'b216', ''),
(10, 1, 1, 12, 5, 3, 0, 0, 'c35', ''),
(11, 1, 1, 11, 7, 3, 0, 2, 'b37', ''),
(12, 1, 1, 13, 9, 3, 0, 0, 'u39', ''),
(13, 1, 1, 14, 11, 3, 0, 6, 's311', ''),
(14, 1, 1, 11, 16, 3, 0, 2, 'b316', ''),
(15, 1, 1, 15, 19, 3, 0, 0, 'A319', ''),
(16, 1, 1, 16, 20, 3, 0, 0, 'z320', ''),
(17, 1, 1, 7, 0, 4, 0, 0, 'a40', ''),
(18, 1, 1, 8, 1, 4, 0, 4, 'y41', ''),
(19, 1, 1, 9, 2, 4, 0, 4, 'w42', ''),
(20, 1, 1, 10, 3, 4, 0, 4, 'v43', ''),
(21, 1, 1, 7, 4, 4, 0, 0, 'a44', ''),
(22, 1, 1, 17, 9, 4, 0, 0, 't49', ''),
(23, 1, 1, 18, 11, 4, 0, 6, 'r411', ''),
(24, 1, 1, 19, 15, 4, 0, 0, 'h415', ''),
(25, 1, 1, 7, 16, 4, 0, 0, 'a416', ''),
(26, 1, 1, 11, 0, 5, 0, 2, 'b50', ''),
(27, 1, 1, 11, 7, 5, 0, 2, 'b57', ''),
(28, 1, 1, 20, 11, 5, 0, 6, 'q511', ''),
(29, 1, 1, 15, 2, 6, 0, 0, 'A62', ''),
(30, 1, 1, 16, 3, 6, 0, 0, 'z63', ''),
(31, 1, 1, 7, 11, 6, 0, 0, 'a611', ''),
(32, 1, 1, 11, 0, 7, 0, 2, 'b70', ''),
(33, 1, 1, 7, 0, 8, 0, 0, 'a80', ''),
(34, 1, 1, 21, 1, 8, 0, 0, 'D81', ''),
(35, 1, 1, 22, 2, 8, 0, 0, 'C82', ''),
(36, 1, 1, 23, 3, 8, 0, 0, 'B83', ''),
(37, 1, 1, 7, 4, 8, 0, 0, 'a84', ''),
(38, 1, 1, 24, 19, 8, 0, 0, 'o819', ''),
(39, 1, 1, 25, 20, 8, 0, 0, 'p820', ''),
(40, 1, 1, 24, 21, 8, 0, 0, 'o821', ''),
(41, 1, 1, 26, 5, 9, 0, 0, 'E95', ''),
(42, 1, 1, 12, 9, 9, 0, 0, 'c99', ''),
(43, 1, 1, 27, 8, 15, 0, 0, 'P158', ''),
(44, 1, 1, 28, 9, 15, 0, 0, 'O159', ''),
(45, 1, 1, 29, 10, 15, 0, 0, 'N1510', ''),
(46, 1, 1, 30, 10, 16, 0, 0, 'M1610', ''),
(47, 1, 1, 31, 10, 17, 0, 0, 'L1710', ''),
(48, 1, 1, 32, 10, 18, 0, 0, 'K1810', ''),
(49, 1, 1, 32, 10, 19, 0, 0, 'K1910', ''),
(50, 1, 1, 32, 10, 20, 0, 0, 'K2010', ''),
(51, 1, 1, 32, 10, 21, 0, 0, 'K2110', ''),
(52, 1, 1, 32, 10, 22, 0, 0, 'K2210', ''),
(53, 1, 1, 32, 10, 23, 0, 0, 'K2310', ''),
(54, 1, 1, 33, 7, 24, 0, 0, 'G247', ''),
(55, 1, 1, 32, 10, 24, 0, 0, 'K2410', ''),
(56, 1, 1, 34, 7, 25, 0, 0, 'F257', ''),
(57, 1, 1, 35, 8, 25, 0, 0, 'H258', ''),
(58, 1, 1, 36, 9, 25, 0, 0, 'I259', ''),
(59, 1, 1, 37, 10, 25, 0, 0, 'J2510', ''),
(60, 1, 1, 38, 12, 27, 0, 0, 'd2712', ''),
(61, 1, 1, 39, 13, 27, 0, 0, 'f2713', ''),
(62, 1, 1, 40, 14, 27, 0, 0, 'e2714', ''),
(63, 1, 1, 38, 15, 27, 0, 0, 'd2715', ''),
(64, 2, 1, 41, 11, 10, 1, 0, 'm1011', ''),
(65, 2, 1, 42, 2, 11, 4, 2, 'd112', ''),
(66, 2, 1, 42, 2, 12, 4, 2, 'd122', ''),
(67, 2, 1, 42, 2, 15, 4, 2, 'd152', ''),
(68, 2, 1, 43, 6, 15, 0, 0, 'c156', ''),
(69, 2, 1, 43, 7, 15, 0, 0, 'c157', ''),
(70, 2, 1, 43, 8, 15, 0, 0, 'c158', ''),
(71, 2, 1, 43, 9, 15, 0, 0, 'c159', ''),
(72, 2, 1, 43, 10, 15, 0, 0, 'c1510', ''),
(73, 2, 1, 43, 12, 15, 0, 0, 'c1512', ''),
(74, 2, 1, 43, 13, 15, 0, 0, 'c1513', ''),
(75, 2, 1, 43, 14, 15, 0, 0, 'c1514', ''),
(76, 2, 1, 43, 15, 15, 0, 0, 'c1515', ''),
(77, 2, 1, 43, 16, 15, 0, 0, 'c1516', ''),
(78, 2, 1, 42, 2, 16, 4, 2, 'd162', ''),
(79, 2, 1, 43, 6, 20, 1, 0, 'c206', ''),
(80, 2, 1, 43, 7, 20, 1, 0, 'c207', ''),
(81, 2, 1, 43, 8, 20, 1, 0, 'c208', ''),
(82, 2, 1, 43, 9, 20, 1, 0, 'c209', ''),
(83, 2, 1, 43, 10, 20, 1, 0, 'c2010', ''),
(84, 2, 1, 43, 12, 20, 1, 0, 'c2012', ''),
(85, 2, 1, 43, 13, 20, 1, 0, 'c2013', ''),
(86, 2, 1, 43, 14, 20, 1, 0, 'c2014', ''),
(87, 2, 1, 43, 15, 20, 1, 0, 'c2015', ''),
(88, 2, 1, 43, 16, 20, 1, 0, 'c2016', ''),
(89, 2, 1, 43, 6, 23, 1, 0, 'c236', ''),
(90, 2, 1, 43, 7, 23, 1, 0, 'c237', ''),
(91, 2, 1, 43, 8, 23, 1, 0, 'c238', ''),
(92, 2, 1, 43, 9, 23, 1, 0, 'c239', ''),
(93, 2, 1, 43, 10, 23, 1, 0, 'c2310', ''),
(94, 2, 1, 43, 12, 23, 1, 0, 'c2312', ''),
(95, 2, 1, 43, 13, 23, 1, 0, 'c2313', ''),
(96, 2, 1, 43, 14, 23, 1, 0, 'c2314', ''),
(97, 2, 1, 43, 15, 23, 1, 0, 'c2315', ''),
(98, 2, 1, 43, 16, 23, 1, 0, 'c2316', ''),
(99, 2, 1, 43, 6, 26, 1, 0, 'c266', ''),
(100, 2, 1, 43, 7, 26, 1, 0, 'c267', ''),
(101, 2, 1, 43, 8, 26, 1, 0, 'c268', ''),
(102, 2, 1, 43, 9, 26, 1, 0, 'c269', ''),
(103, 2, 1, 43, 10, 26, 1, 0, 'c2610', ''),
(104, 2, 1, 43, 12, 26, 1, 0, 'c2612', ''),
(105, 2, 1, 43, 13, 26, 1, 0, 'c2613', ''),
(106, 2, 1, 43, 14, 26, 1, 0, 'c2614', ''),
(107, 2, 1, 43, 15, 26, 1, 0, 'c2615', ''),
(108, 2, 1, 43, 16, 26, 1, 0, 'c2616', ''),
(109, 3, 1, 44, 13, 1, 3, 4, 'h113', ''),
(110, 3, 1, 45, 14, 1, 3, 4, 'i114', ''),
(111, 3, 1, 46, 15, 1, 3, 4, 'j115', ''),
(112, 3, 1, 47, 16, 1, 3, 0, 'c116', ''),
(113, 3, 1, 44, 18, 1, 3, 4, 'h118', ''),
(114, 3, 1, 45, 19, 1, 3, 4, 'i119', ''),
(115, 3, 1, 46, 20, 1, 3, 4, 'j120', ''),
(116, 3, 1, 48, 13, 3, 3, 0, 'l313', ''),
(117, 3, 1, 49, 15, 3, 3, 0, 'k315', ''),
(118, 3, 1, 48, 18, 3, 3, 0, 'l318', ''),
(119, 3, 1, 49, 20, 3, 3, 0, 'k320', ''),
(120, 3, 1, 50, 2, 6, 3, 4, 'e62', ''),
(121, 3, 1, 50, 3, 6, 3, 4, 'e63', ''),
(122, 3, 1, 51, 8, 6, 3, 4, 'f68', ''),
(123, 3, 1, 51, 9, 6, 3, 4, 'f69', ''),
(124, 3, 1, 51, 10, 6, 3, 4, 'f610', ''),
(125, 3, 1, 51, 11, 6, 3, 4, 'f611', ''),
(126, 3, 1, 52, 2, 8, 3, 0, 'a82', ''),
(127, 3, 1, 53, 3, 8, 3, 0, 'b83', ''),
(128, 3, 1, 54, 2, 9, 3, 0, 'm92', ''),
(129, 3, 1, 54, 3, 9, 3, 0, 'm93', ''),
(130, 3, 1, 50, 16, 9, 3, 4, 'e916', ''),
(131, 3, 1, 50, 17, 9, 3, 4, 'e917', ''),
(132, 3, 1, 52, 16, 11, 3, 0, 'a1116', ''),
(133, 3, 1, 53, 17, 11, 3, 0, 'b1117', ''),
(134, 3, 1, 50, 2, 12, 3, 4, 'e122', ''),
(135, 3, 1, 50, 3, 12, 3, 4, 'e123', ''),
(136, 3, 1, 54, 16, 12, 3, 0, 'm1216', ''),
(137, 3, 1, 54, 17, 12, 3, 0, 'm1217', ''),
(138, 3, 1, 52, 2, 14, 3, 0, 'a142', ''),
(139, 3, 1, 53, 3, 14, 3, 0, 'b143', ''),
(140, 3, 1, 54, 2, 15, 3, 0, 'm152', ''),
(141, 3, 1, 54, 3, 15, 3, 0, 'm153', ''),
(142, 3, 1, 55, 0, 18, 1, 0, 'd180', ''),
(143, 3, 1, 56, 0, 19, 1, 2, 'g190', ''),
(144, 3, 1, 56, 0, 20, 1, 2, 'g200', ''),
(145, 3, 1, 55, 0, 21, 1, 0, 'd210', '');

-- --------------------------------------------------------

--
-- Table structure for table `item_definitions`
--

CREATE TABLE `item_definitions` (
  `id` int(11) NOT NULL,
  `swf_name` varchar(30) NOT NULL,
  `sprite_type` varchar(1) NOT NULL,
  `sprite_id` int(11) NOT NULL,
  `length` int(11) NOT NULL,
  `width` int(11) NOT NULL,
  `height` double NOT NULL,
  `can_stack` tinyint(1) NOT NULL,
  `item_type` varchar(20) NOT NULL,
  `can_gift` tinyint(1) NOT NULL,
  `can_recycle` tinyint(1) NOT NULL,
  `interactor_requires_rights` tinyint(1) NOT NULL DEFAULT '0',
  `interactor_type` varchar(20) NOT NULL,
  `vendor_ids` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `item_definitions`
--

INSERT INTO `item_definitions` (`id`, `swf_name`, `sprite_type`, `sprite_id`, `length`, `width`, `height`, `can_stack`, `item_type`, `can_gift`, `can_recycle`, `interactor_requires_rights`, `interactor_type`, `vendor_ids`) VALUES
(1, 'throne', 's', 230, 1, 1, 1, 0, 'seat', 1, 0, 0, 'none', ''),
(2, 'typingmachine', 's', 254, 1, 1, 0, 0, 'solid', 1, 0, 0, 'none', ''),
(3, 'poster', 'i', 4001, 0, 0, 0, 0, 'poster', 1, 1, 0, 'none', ''),
(4, 'prizetrophy*1', 's', 1512, 1, 1, 0, 0, 'trophy', 1, 0, 0, 'none', ''),
(5, 'prizetrophy*2', 's', 1519, 1, 1, 0, 0, 'trophy', 1, 0, 0, 'none', ''),
(6, 'prizetrophy*3', 's', 1526, 1, 1, 0, 0, 'trophy', 1, 0, 0, 'none', ''),
(7, 'crl_lamp', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(8, 'crl_sofa2c', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(9, 'crl_sofa2b', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(10, 'crl_sofa2a', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(11, 'crl_chair', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(12, 'crl_pillar', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(13, 'crl_table1b', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(14, 'crl_sofa1c', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(15, 'crl_table2b', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(16, 'crl_table2a', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(17, 'crl_table1a', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(18, 'crl_sofa1b', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(19, 'crl_wall2a', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(20, 'crl_sofa1a', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(21, 'crl_sofa3c', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(22, 'crl_sofa3b', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(23, 'crl_sofa3a', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(24, 'crl_barchair2', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(25, 'crl_tablebar', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(26, 'crl_pillar2', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(27, 'crl_desk1a', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(28, 'crl_deski', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(29, 'crl_deskh', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(30, 'crl_deskg', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(31, 'crl_deskf', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(32, 'crl_deske', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(33, 'crl_wallb', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(34, 'crl_walla', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(35, 'crl_desk1b', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(36, 'crl_desk1c', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(37, 'crl_desk1d', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(38, 'crl_lamp2', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(39, 'crl_cabinet2', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(40, 'crl_cabinet1', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(41, 'mic', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(42, 'thchair2', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(43, 'thchair1', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(44, 'hardwoodsofa1', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(45, 'hardwoodsofa2', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(46, 'hardwoodsofa3', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(47, 'teabamboo', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(48, 'teasmalltable1', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(49, 'teasmalltable2', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(50, 'teastool', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(51, 'chinastoolred', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(52, 'teatable1', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(53, 'teatable2', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(54, 'teastool2', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', ''),
(55, 'teavase', '', 0, 1, 1, 0, 0, 'solid', 0, 0, 0, 'none', ''),
(56, 'chinastoolgreen', '', 0, 1, 1, 0, 0, 'seat', 0, 0, 0, 'none', '');

-- --------------------------------------------------------

--
-- Table structure for table `messenger_friends`
--

CREATE TABLE `messenger_friends` (
  `user_one_id` int(11) NOT NULL,
  `user_two_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `messenger_friends`
--

INSERT INTO `messenger_friends` (`user_one_id`, `user_two_id`) VALUES
(1, 2),
(2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `messenger_requests`
--

CREATE TABLE `messenger_requests` (
  `id` int(11) UNSIGNED NOT NULL,
  `from_id` int(11) NOT NULL DEFAULT '0',
  `to_id` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `id` int(11) NOT NULL,
  `username` varchar(15) NOT NULL,
  `password` varchar(80) NOT NULL,
  `email` varchar(30) NOT NULL,
  `gender` varchar(1) NOT NULL,
  `figure` varchar(80) NOT NULL,
  `motto` varchar(40) NOT NULL,
  `coins` int(11) NOT NULL DEFAULT '500',
  `pixels` int(11) NOT NULL DEFAULT '0',
  `rank` tinyint(3) UNSIGNED NOT NULL DEFAULT '1',
  `home_room` int(11) NOT NULL DEFAULT '0',
  `sso_ticket` varchar(40) NOT NULL,
  `online` enum('1','0') NOT NULL DEFAULT '0',
  `block_friendrequests` int(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `players`
--

INSERT INTO `players` (`id`, `username`, `password`, `email`, `gender`, `figure`, `motto`, `coins`, `pixels`, `rank`, `home_room`, `sso_ticket`, `online`, `block_friendrequests`) VALUES
(1, 'Raiden', '123', 'joshua@aurora-dev.com', 'M', 'hd-180-1.ch-210-66.lg-270-82.sh-290-91.hr-100', 'Because it matters...', 477, 50, 7, 1, 'beta', '0', 0),
(2, 'Spreed', '123', 'spreed@spreed.com', 'M', 'hd-180-1.ch-210-66.lg-270-82.sh-290-91.hr-100', 'Because it matters...', 1500, 150, 7, 0, 'spreed', '1', 0),
(3, 'Testing', '123', 'test@test.com', 'F', 'hr-100-.hd-180-1.ch-876-66.lg-270-94.sh-300-64', 'I\'m here for tersting', 500, 0, 1, 0, 'test', '0', 0),
(4, 'Alex', '123', 'duck@quakc.com', 'N', 'hd-180-1.ch-210-66.lg-270-82.sh-290-91.hr-100', 'I\'m here for roast duck.', 475, 200, 7, 0, '123', '0', 0);

-- --------------------------------------------------------

--
-- Table structure for table `player_badges`
--

CREATE TABLE `player_badges` (
  `id` int(11) NOT NULL,
  `player_id` int(11) NOT NULL,
  `badge_code` varchar(30) NOT NULL,
  `slot_number` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `player_badges`
--

INSERT INTO `player_badges` (`id`, `player_id`, `badge_code`, `slot_number`) VALUES
(1, 1, 'ADM', 1),
(2, 1, 'EXH', 2);

-- --------------------------------------------------------

--
-- Table structure for table `player_subscriptions`
--

CREATE TABLE `player_subscriptions` (
  `id` int(11) UNSIGNED NOT NULL,
  `user_id` int(11) NOT NULL DEFAULT '0',
  `subscription_id` varchar(15) NOT NULL DEFAULT '',
  `timestamp_bought` int(20) NOT NULL DEFAULT '0',
  `timestamp_expire` int(20) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `player_subscriptions`
--

INSERT INTO `player_subscriptions` (`id`, `user_id`, `subscription_id`, `timestamp_bought`, `timestamp_expire`) VALUES
(1, 1, 'habbo_club', 1504395758, 1504397058);

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `id` int(11) NOT NULL,
  `owner_id` int(11) DEFAULT NULL,
  `name` varchar(30) NOT NULL,
  `description` tinytext NOT NULL,
  `state` varchar(8) NOT NULL DEFAULT 'open',
  `players_in` int(3) NOT NULL DEFAULT '0',
  `players_max` int(3) NOT NULL DEFAULT '25',
  `category_id` int(3) NOT NULL DEFAULT '0',
  `model` varchar(30) NOT NULL,
  `ccts` tinytext NOT NULL,
  `show_owner` tinyint(1) NOT NULL DEFAULT '1',
  `all_player_rights` tinyint(1) NOT NULL DEFAULT '1',
  `icon` varchar(20) NOT NULL DEFAULT 'HHIPAI',
  `floor` int(3) NOT NULL DEFAULT '0',
  `wallpaper` int(3) NOT NULL DEFAULT '0',
  `landscape` double NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`id`, `owner_id`, `name`, `description`, `state`, `players_in`, `players_max`, `category_id`, `model`, `ccts`, `show_owner`, `all_player_rights`, `icon`, `floor`, `wallpaper`, `landscape`) VALUES
(1, 1, 'Welcome Lounge', 'New? Lost? Get a warm welcome here!', 'open', 0, 80, 0, 'newbie_lobby', 'hh_room_nlobby', 0, 0, 'HHIPAI', 0, 0, 0),
(2, 4, 'Theatredrome', 'Join in all the fun of the fair!', 'open', 0, 80, 0, 'theater', 'hh_room_theater', 0, 0, 'HHIPAI', 0, 0, 0),
(3, 4, 'Chinese Tea Room', 'Try the tea in this Mongol cafe - it is to die for darlings!', 'open', 0, 80, 0, 'tearoom', 'hh_room_tearoom', 0, 0, 'HHIPAI', 0, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `room_categories`
--

CREATE TABLE `room_categories` (
  `id` int(3) NOT NULL,
  `name` varchar(30) NOT NULL,
  `min_rank` int(2) NOT NULL,
  `trade_allowed` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `room_categories`
--

INSERT INTO `room_categories` (`id`, `name`, `min_rank`, `trade_allowed`) VALUES
(0, 'No Category', 1, 0),
(1, 'Staff Zone', 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `room_maps`
--

CREATE TABLE `room_maps` (
  `name` varchar(20) NOT NULL,
  `door_x` int(3) NOT NULL,
  `door_y` int(3) NOT NULL,
  `door_z` double NOT NULL,
  `door_rotation` int(2) NOT NULL,
  `raw_map` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `room_maps`
--

INSERT INTO `room_maps` (`name`, `door_x`, `door_y`, `door_z`, `door_rotation`, `raw_map`) VALUES
('model_a', 3, 5, 0, 2, 'xxxxxxxxxxxx|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxxxxxxxxxx|xxxxxxxxxxxx'),
('model_b', 0, 5, 0, 2, 'xxxxxxxxxxxx|xxxxx0000000|xxxxx0000000|xxxxx0000000|xxxxx0000000|x00000000000|x00000000000|x00000000000|x00000000000|x00000000000|x00000000000|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx'),
('model_c', 4, 7, 0, 2, 'xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx'),
('model_g', 1, 7, 1, 2, 'xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxx00000|xxxxxxx00000|xxxxxxx00000|xx1111000000|xx1111000000|xx1111000000|xx1111000000|xx1111000000|xxxxxxx00000|xxxxxxx00000|xxxxxxx00000|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx'),
('model_n', 0, 16, 0, 2, 'xxxxxxxxxxxxxxxxxxxxx|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x000000xxxxxxxx000000|x000000x000000x000000|x000000x000000x000000|x000000x000000x000000|x000000x000000x000000|x000000x000000x000000|x000000x000000x000000|x000000xxxxxxxx000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|xxxxxxxxxxxxxxxxxxxxx'),
('newbie_lobby', 2, 11, 0, 2, 'xxxxxxxxxxxxxxxx000000|xxxxx0xxxxxxxxxx000000|xxxxx00000000xxx000000|xxxxx000000000xx000000|0000000000000000000000|0000000000000000000000|0000000000000000000000|0000000000000000000000|0000000000000000000000|xxxxx000000000000000xx|xxxxx000000000000000xx|x0000000000000000000xx|x0000000000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxxx0000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxx00000000000xxxxx|xxxxx000000000000xxxxx|xxxxx000000000000xxxxx'),
('tearoom', 21, 19, 1, 6, 'xxxxxxxxxxxxxxxxxxxxxx|xxxxxxxx3333x33333333x|333333xx3333x33333333x|3333333x3333x33333333x|3333333x3333x33333333x|3333333xxxxxx33333333x|333333333333333333333x|333333333333333333333x|333333333333333333333x|333333333333333333333x|33333333222x333333333x|33333333222x333333333x|33333333222x333333333x|33333333222x333333333x|33333333111x333333333x|33333333111x333333333x|33333333111x333333333x|xxxxxxxx111xxxxxxxxxxx|11111111111111111111xx|1111111111111111111111|1111111111111111111111|11111111111111111111xx'),
('theater', 20, 27, 0, 0, 'xxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxxxxxxxxxxxxxx|xxxxxxx111111111xxxxxxx|xxxxxxx11111111100000xx|xxxx00x11111111100000xx|xxxx00x11111111100000xx|4xxx00x11111111100000xx|4440000xxxxxxxxx00000xx|444000000000000000000xx|4xx000000000000000000xx|4xx0000000000000000000x|44400000000000000000000|44400000000000000000000|44x0000000000000000o000|44x11111111111111111000|44x11111111111111111000|33x11111111111111111000|22x11111111111111111000|22x11111111111111111000|22x11111111111111111000|22x11111111111111111000|22x11111111111111111000|22211111111111111111000|22211111111111111111000|xxxxxxxxxxxxxxxxxxxx00x|xxxxxxxxxxxxxxxxxxxx00x');

-- --------------------------------------------------------

--
-- Table structure for table `wordfilter`
--

CREATE TABLE `wordfilter` (
  `not_allowed_message` varchar(255) NOT NULL,
  `replace_message` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `wordfilter`
--

INSERT INTO `wordfilter` (`not_allowed_message`, `replace_message`) VALUES
('hello', 'fuck you');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `catalog_deals`
--
ALTER TABLE `catalog_deals`
  ADD PRIMARY KEY (`id`,`template_id`),
  ADD KEY `template_id` (`template_id`);

--
-- Indexes for table `catalog_pages`
--
ALTER TABLE `catalog_pages`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `catalog_pages_data`
--
ALTER TABLE `catalog_pages_data`
  ADD PRIMARY KEY (`id`),
  ADD KEY `page_id` (`page_id`),
  ADD KEY `page_id_2` (`page_id`);

--
-- Indexes for table `catalog_products`
--
ALTER TABLE `catalog_products`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Template_id` (`template_id`),
  ADD KEY `Template_id_2` (`template_id`),
  ADD KEY `Template_id_3` (`template_id`),
  ADD KEY `Template_id_4` (`template_id`),
  ADD KEY `Template_id_5` (`template_id`),
  ADD KEY `Template_id_6` (`template_id`),
  ADD KEY `Template_id_7` (`template_id`),
  ADD KEY `Template_id_8` (`template_id`),
  ADD KEY `Template_id_9` (`template_id`),
  ADD KEY `page_id` (`page_id`),
  ADD KEY `deal_id` (`deal_id`);

--
-- Indexes for table `catalog_vouchers`
--
ALTER TABLE `catalog_vouchers`
  ADD PRIMARY KEY (`voucher`);

--
-- Indexes for table `frontpage_items`
--
ALTER TABLE `frontpage_items`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`),
  ADD KEY `room_id` (`room_id`),
  ADD KEY `owner_id` (`owner_id`),
  ADD KEY `definition_id` (`definition_id`);

--
-- Indexes for table `item_definitions`
--
ALTER TABLE `item_definitions`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `messenger_friends`
--
ALTER TABLE `messenger_friends`
  ADD PRIMARY KEY (`user_one_id`,`user_two_id`);

--
-- Indexes for table `messenger_requests`
--
ALTER TABLE `messenger_requests`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indexes for table `player_badges`
--
ALTER TABLE `player_badges`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `player_id` (`player_id`,`badge_code`);

--
-- Indexes for table `player_subscriptions`
--
ALTER TABLE `player_subscriptions`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`id`),
  ADD KEY `owner_id` (`owner_id`);

--
-- Indexes for table `room_categories`
--
ALTER TABLE `room_categories`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indexes for table `room_maps`
--
ALTER TABLE `room_maps`
  ADD PRIMARY KEY (`name`);

--
-- Indexes for table `wordfilter`
--
ALTER TABLE `wordfilter`
  ADD PRIMARY KEY (`not_allowed_message`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `catalog_pages`
--
ALTER TABLE `catalog_pages`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `catalog_pages_data`
--
ALTER TABLE `catalog_pages_data`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
--
-- AUTO_INCREMENT for table `catalog_products`
--
ALTER TABLE `catalog_products`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `frontpage_items`
--
ALTER TABLE `frontpage_items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=146;
--
-- AUTO_INCREMENT for table `item_definitions`
--
ALTER TABLE `item_definitions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=57;
--
-- AUTO_INCREMENT for table `messenger_requests`
--
ALTER TABLE `messenger_requests`
  MODIFY `id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `players`
--
ALTER TABLE `players`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `player_badges`
--
ALTER TABLE `player_badges`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `player_subscriptions`
--
ALTER TABLE `player_subscriptions`
  MODIFY `id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `rooms`
--
ALTER TABLE `rooms`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `room_categories`
--
ALTER TABLE `room_categories`
  MODIFY `id` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `catalog_deals`
--
ALTER TABLE `catalog_deals`
  ADD CONSTRAINT `catalog_deals_ibfk_1` FOREIGN KEY (`template_id`) REFERENCES `item_definitions` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `catalog_pages_data`
--
ALTER TABLE `catalog_pages_data`
  ADD CONSTRAINT `FKD3CDD5B9CCA1676B` FOREIGN KEY (`page_id`) REFERENCES `catalog_pages` (`id`);

--
-- Constraints for table `catalog_products`
--
ALTER TABLE `catalog_products`
  ADD CONSTRAINT `catalog_products_ibfk_1` FOREIGN KEY (`template_id`) REFERENCES `item_definitions` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `catalog_products_ibfk_2` FOREIGN KEY (`page_id`) REFERENCES `catalog_pages` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `catalog_products_ibfk_3` FOREIGN KEY (`deal_id`) REFERENCES `catalog_deals` (`id`);

--
-- Constraints for table `items`
--
ALTER TABLE `items`
  ADD CONSTRAINT `items_ibfk_1` FOREIGN KEY (`room_id`) REFERENCES `rooms` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `items_ibfk_2` FOREIGN KEY (`owner_id`) REFERENCES `players` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `items_ibfk_3` FOREIGN KEY (`definition_id`) REFERENCES `item_definitions` (`id`);

--
-- Constraints for table `rooms`
--
ALTER TABLE `rooms`
  ADD CONSTRAINT `rooms_ibfk_1` FOREIGN KEY (`owner_id`) REFERENCES `players` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
