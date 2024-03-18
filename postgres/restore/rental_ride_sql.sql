--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2024-03-18 12:25:42

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

--CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 4918 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 851 (class 1247 OID 24578)
-- Name: access_toggle; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.access_toggle AS ENUM (
    'enabled',
    'disabled'
);


ALTER TYPE public.access_toggle OWNER TO postgres;

--
-- TOC entry 854 (class 1247 OID 24584)
-- Name: cnh; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.cnh AS ENUM (
    'A',
    'B',
    'A+B'
);


ALTER TYPE public.cnh OWNER TO postgres;

--
-- TOC entry 857 (class 1247 OID 24592)
-- Name: permission_type; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.permission_type AS ENUM (
    'admin',
    'deliverer'
);


ALTER TYPE public.permission_type OWNER TO postgres;

--
-- TOC entry 860 (class 1247 OID 24598)
-- Name: status; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.status AS ENUM (
    'available',
    'accepted',
    'delivered'
);


ALTER TYPE public.status OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 24605)
-- Name: deliverer; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.deliverer (
    id_del bigint NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    date_of_birth date,
    drivers_license character varying(50) NOT NULL,
    license_photo_url character varying(255),
    license_type integer,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    cnpj character varying(20)
);


ALTER TABLE public.deliverer OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 24609)
-- Name: deliverer_id_del_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.deliverer_id_del_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.deliverer_id_del_seq OWNER TO postgres;

--
-- TOC entry 4919 (class 0 OID 0)
-- Dependencies: 216
-- Name: deliverer_id_del_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.deliverer_id_del_seq OWNED BY public.deliverer.id_del;


--
-- TOC entry 226 (class 1259 OID 24682)
-- Name: delivery; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.delivery (
    id bigint NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    deliverer_id bigint,
    current_status integer DEFAULT 1 NOT NULL,
    delivery_cost numeric
);


ALTER TABLE public.delivery OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 24681)
-- Name: delivery_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.delivery_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.delivery_id_seq OWNER TO postgres;

--
-- TOC entry 4920 (class 0 OID 0)
-- Dependencies: 225
-- Name: delivery_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.delivery_id_seq OWNED BY public.delivery.id;


--
-- TOC entry 217 (class 1259 OID 24618)
-- Name: motorcycle; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.motorcycle (
    id bigint NOT NULL,
    model character varying(50) NOT NULL,
    year integer NOT NULL,
    license_plate character varying(10) NOT NULL,
    times_rented integer
);


ALTER TABLE public.motorcycle OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 24621)
-- Name: motorcycle_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.motorcycle_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.motorcycle_id_seq OWNER TO postgres;

--
-- TOC entry 4921 (class 0 OID 0)
-- Dependencies: 218
-- Name: motorcycle_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.motorcycle_id_seq OWNED BY public.motorcycle.id;


--
-- TOC entry 219 (class 1259 OID 24622)
-- Name: reservation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.reservation (
    id bigint NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    start_date date,
    estimated_end_date date,
    end_date date,
    motorcycle bigint,
    reservation_plan_id bigint NOT NULL,
    status integer DEFAULT 0,
    deliverer_id integer NOT NULL
);


ALTER TABLE public.reservation OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 24627)
-- Name: reservation_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.reservation_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.reservation_id_seq OWNER TO postgres;

--
-- TOC entry 4922 (class 0 OID 0)
-- Dependencies: 220
-- Name: reservation_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.reservation_id_seq OWNED BY public.reservation.id;


--
-- TOC entry 221 (class 1259 OID 24628)
-- Name: reservation_plan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.reservation_plan (
    id bigint NOT NULL,
    name character varying(50) NOT NULL,
    rental_days integer NOT NULL,
    daily_cost numeric NOT NULL,
    percentage_fine numeric
);


ALTER TABLE public.reservation_plan OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 24633)
-- Name: reservation_plan_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.reservation_plan_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.reservation_plan_id_seq OWNER TO postgres;

--
-- TOC entry 4923 (class 0 OID 0)
-- Dependencies: 222
-- Name: reservation_plan_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.reservation_plan_id_seq OWNED BY public.reservation_plan.id;


--
-- TOC entry 223 (class 1259 OID 24634)
-- Name: userbase; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.userbase (
    id bigint NOT NULL,
    username character varying(50) NOT NULL,
    password character varying(255) NOT NULL,
    deliverer_id bigint,
    email character varying(255),
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    active_access integer,
    access_level integer
);


ALTER TABLE public.userbase OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 24640)
-- Name: userbase_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.userbase_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.userbase_id_seq OWNER TO postgres;

--
-- TOC entry 4924 (class 0 OID 0)
-- Dependencies: 224
-- Name: userbase_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.userbase_id_seq OWNED BY public.userbase.id;


--
-- TOC entry 4725 (class 2604 OID 24641)
-- Name: deliverer id_del; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deliverer ALTER COLUMN id_del SET DEFAULT nextval('public.deliverer_id_del_seq'::regclass);


--
-- TOC entry 4734 (class 2604 OID 24685)
-- Name: delivery id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.delivery ALTER COLUMN id SET DEFAULT nextval('public.delivery_id_seq'::regclass);


--
-- TOC entry 4727 (class 2604 OID 24643)
-- Name: motorcycle id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.motorcycle ALTER COLUMN id SET DEFAULT nextval('public.motorcycle_id_seq'::regclass);


--
-- TOC entry 4728 (class 2604 OID 24644)
-- Name: reservation id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation ALTER COLUMN id SET DEFAULT nextval('public.reservation_id_seq'::regclass);


--
-- TOC entry 4731 (class 2604 OID 24645)
-- Name: reservation_plan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation_plan ALTER COLUMN id SET DEFAULT nextval('public.reservation_plan_id_seq'::regclass);


--
-- TOC entry 4732 (class 2604 OID 24646)
-- Name: userbase id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.userbase ALTER COLUMN id SET DEFAULT nextval('public.userbase_id_seq'::regclass);


--
-- TOC entry 4901 (class 0 OID 24605)
-- Dependencies: 215
-- Data for Name: deliverer; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.deliverer (id_del, first_name, last_name, date_of_birth, drivers_license, license_photo_url, license_type, created_at, cnpj) FROM stdin;
14	teste	teste	2024-03-12	teste	teste	1	2024-03-12 20:38:14.575031	\N
\.


--
-- TOC entry 4912 (class 0 OID 24682)
-- Dependencies: 226
-- Data for Name: delivery; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.delivery (id, created_at, deliverer_id, current_status, delivery_cost) FROM stdin;
\.


--
-- TOC entry 4903 (class 0 OID 24618)
-- Dependencies: 217
-- Data for Name: motorcycle; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.motorcycle (id, model, year, license_plate, times_rented) FROM stdin;
1	X-Moto	1999	GHP-1345	\N
2	x-moto	1991	NHA-UWU	\N
\.


--
-- TOC entry 4905 (class 0 OID 24622)
-- Dependencies: 219
-- Data for Name: reservation; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.reservation (id, created_at, start_date, estimated_end_date, end_date, motorcycle, reservation_plan_id, status, deliverer_id) FROM stdin;
2	2024-03-13 00:26:47.317858	2024-03-13	2024-03-20	\N	1	1	0	1
\.


--
-- TOC entry 4907 (class 0 OID 24628)
-- Dependencies: 221
-- Data for Name: reservation_plan; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.reservation_plan (id, name, rental_days, daily_cost, percentage_fine) FROM stdin;
1	7 dias com um custo de R$30,00 por dia	7	30.00	\N
\.


--
-- TOC entry 4909 (class 0 OID 24634)
-- Dependencies: 223
-- Data for Name: userbase; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.userbase (id, username, password, deliverer_id, email, created_at, active_access, access_level) FROM stdin;
3	teste	teste	1	teste	2024-03-12 20:38:14.579314	\N	1
\.


--
-- TOC entry 4925 (class 0 OID 0)
-- Dependencies: 216
-- Name: deliverer_id_del_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.deliverer_id_del_seq', 14, true);


--
-- TOC entry 4926 (class 0 OID 0)
-- Dependencies: 225
-- Name: delivery_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.delivery_id_seq', 1, false);


--
-- TOC entry 4927 (class 0 OID 0)
-- Dependencies: 218
-- Name: motorcycle_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.motorcycle_id_seq', 2, true);


--
-- TOC entry 4928 (class 0 OID 0)
-- Dependencies: 220
-- Name: reservation_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.reservation_id_seq', 2, true);


--
-- TOC entry 4929 (class 0 OID 0)
-- Dependencies: 222
-- Name: reservation_plan_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.reservation_plan_id_seq', 1, true);


--
-- TOC entry 4930 (class 0 OID 0)
-- Dependencies: 224
-- Name: userbase_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.userbase_id_seq', 3, true);


--
-- TOC entry 4738 (class 2606 OID 24698)
-- Name: deliverer deliverer_cnpj_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deliverer
    ADD CONSTRAINT deliverer_cnpj_key UNIQUE (cnpj);


--
-- TOC entry 4740 (class 2606 OID 24648)
-- Name: deliverer deliverer_drivers_license_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deliverer
    ADD CONSTRAINT deliverer_drivers_license_key UNIQUE (drivers_license);


--
-- TOC entry 4742 (class 2606 OID 24650)
-- Name: deliverer deliverer_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deliverer
    ADD CONSTRAINT deliverer_pkey PRIMARY KEY (id_del);


--
-- TOC entry 4754 (class 2606 OID 24691)
-- Name: delivery delivery_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.delivery
    ADD CONSTRAINT delivery_pkey PRIMARY KEY (id);


--
-- TOC entry 4744 (class 2606 OID 24654)
-- Name: motorcycle motorcycle_license_plate_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.motorcycle
    ADD CONSTRAINT motorcycle_license_plate_key UNIQUE (license_plate);


--
-- TOC entry 4746 (class 2606 OID 24656)
-- Name: motorcycle motorcycle_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.motorcycle
    ADD CONSTRAINT motorcycle_pkey PRIMARY KEY (id);


--
-- TOC entry 4748 (class 2606 OID 24658)
-- Name: reservation reservation_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_pkey PRIMARY KEY (id);


--
-- TOC entry 4750 (class 2606 OID 24660)
-- Name: reservation_plan reservation_plan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation_plan
    ADD CONSTRAINT reservation_plan_pkey PRIMARY KEY (id);


--
-- TOC entry 4752 (class 2606 OID 24662)
-- Name: userbase userbase_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.userbase
    ADD CONSTRAINT userbase_pkey PRIMARY KEY (id);


--
-- TOC entry 4757 (class 2606 OID 24692)
-- Name: delivery delivery_deliverer_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.delivery
    ADD CONSTRAINT delivery_deliverer_id_fkey FOREIGN KEY (deliverer_id) REFERENCES public.deliverer(id_del);


--
-- TOC entry 4755 (class 2606 OID 24668)
-- Name: reservation reservation_motorcycle_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_motorcycle_fkey FOREIGN KEY (motorcycle) REFERENCES public.motorcycle(id);


--
-- TOC entry 4756 (class 2606 OID 24673)
-- Name: reservation reservation_reservation_plan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_reservation_plan_id_fkey FOREIGN KEY (reservation_plan_id) REFERENCES public.reservation_plan(id);


-- Completed on 2024-03-18 12:25:43

--
-- PostgreSQL database dump complete
--

