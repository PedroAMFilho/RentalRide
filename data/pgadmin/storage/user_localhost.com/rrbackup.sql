PGDMP                      |         
   RentalRide    16.2    16.2 7    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16397 
   RentalRide    DATABASE     �   CREATE DATABASE "RentalRide" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Portuguese_Brazil.932';
    DROP DATABASE "RentalRide";
                postgres    false            e           1247    16514    access_toggle    TYPE     L   CREATE TYPE public.access_toggle AS ENUM (
    'enabled',
    'disabled'
);
     DROP TYPE public.access_toggle;
       public          postgres    false            \           1247    16478    cnh    TYPE     @   CREATE TYPE public.cnh AS ENUM (
    'A',
    'B',
    'A+B'
);
    DROP TYPE public.cnh;
       public          postgres    false            b           1247    16508    permission_type    TYPE     M   CREATE TYPE public.permission_type AS ENUM (
    'admin',
    'deliverer'
);
 "   DROP TYPE public.permission_type;
       public          postgres    false            Y           1247    16453    status    TYPE     X   CREATE TYPE public.status AS ENUM (
    'available',
    'accepted',
    'delivered'
);
    DROP TYPE public.status;
       public          postgres    false            �            1259    16561 	   deliverer    TABLE     u  CREATE TABLE public.deliverer (
    id_del bigint NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    date_of_birth date,
    drivers_license character varying(50) NOT NULL,
    license_photo_url character varying(255),
    license_type integer,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);
    DROP TABLE public.deliverer;
       public         heap    postgres    false            �            1259    16560    deliverer_id_del_seq    SEQUENCE     }   CREATE SEQUENCE public.deliverer_id_del_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.deliverer_id_del_seq;
       public          postgres    false    224            �           0    0    deliverer_id_del_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.deliverer_id_del_seq OWNED BY public.deliverer.id_del;
          public          postgres    false    223            �            1259    16591    delivery    TABLE     �   CREATE TABLE public.delivery (
    id bigint NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    deliverer_id bigint NOT NULL,
    current_status integer DEFAULT 1 NOT NULL,
    delivery_cost numeric
);
    DROP TABLE public.delivery;
       public         heap    postgres    false            �            1259    16590    delivery_id_seq    SEQUENCE     x   CREATE SEQUENCE public.delivery_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.delivery_id_seq;
       public          postgres    false    226                        0    0    delivery_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.delivery_id_seq OWNED BY public.delivery.id;
          public          postgres    false    225            �            1259    16421 
   motorcycle    TABLE     �   CREATE TABLE public.motorcycle (
    id bigint NOT NULL,
    model character varying(50) NOT NULL,
    year integer NOT NULL,
    license_plate character varying(10) NOT NULL,
    times_rented integer
);
    DROP TABLE public.motorcycle;
       public         heap    postgres    false            �            1259    16420    motorcycle_id_seq    SEQUENCE     z   CREATE SEQUENCE public.motorcycle_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.motorcycle_id_seq;
       public          postgres    false    216                       0    0    motorcycle_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.motorcycle_id_seq OWNED BY public.motorcycle.id;
          public          postgres    false    215            �            1259    16490    reservation    TABLE     H  CREATE TABLE public.reservation (
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
    DROP TABLE public.reservation;
       public         heap    postgres    false            �            1259    16489    reservation_id_seq    SEQUENCE     {   CREATE SEQUENCE public.reservation_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.reservation_id_seq;
       public          postgres    false    220                       0    0    reservation_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.reservation_id_seq OWNED BY public.reservation.id;
          public          postgres    false    219            �            1259    16430    reservation_plan    TABLE     �   CREATE TABLE public.reservation_plan (
    id bigint NOT NULL,
    name character varying(50) NOT NULL,
    rental_days integer NOT NULL,
    daily_cost numeric NOT NULL
);
 $   DROP TABLE public.reservation_plan;
       public         heap    postgres    false            �            1259    16429    reservation_plan_id_seq    SEQUENCE     �   CREATE SEQUENCE public.reservation_plan_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.reservation_plan_id_seq;
       public          postgres    false    218                       0    0    reservation_plan_id_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.reservation_plan_id_seq OWNED BY public.reservation_plan.id;
          public          postgres    false    217            �            1259    16520    userbase    TABLE     J  CREATE TABLE public.userbase (
    id bigint NOT NULL,
    username character varying(50) NOT NULL,
    password character varying(255) NOT NULL,
    deliverer_id bigint,
    email character varying(255),
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    active_access integer,
    access_level integer
);
    DROP TABLE public.userbase;
       public         heap    postgres    false            �            1259    16519    userbase_id_seq    SEQUENCE     x   CREATE SEQUENCE public.userbase_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.userbase_id_seq;
       public          postgres    false    222                       0    0    userbase_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.userbase_id_seq OWNED BY public.userbase.id;
          public          postgres    false    221            F           2604    16564    deliverer id_del    DEFAULT     t   ALTER TABLE ONLY public.deliverer ALTER COLUMN id_del SET DEFAULT nextval('public.deliverer_id_del_seq'::regclass);
 ?   ALTER TABLE public.deliverer ALTER COLUMN id_del DROP DEFAULT;
       public          postgres    false    224    223    224            H           2604    16594    delivery id    DEFAULT     j   ALTER TABLE ONLY public.delivery ALTER COLUMN id SET DEFAULT nextval('public.delivery_id_seq'::regclass);
 :   ALTER TABLE public.delivery ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    226    225    226            ?           2604    16424    motorcycle id    DEFAULT     n   ALTER TABLE ONLY public.motorcycle ALTER COLUMN id SET DEFAULT nextval('public.motorcycle_id_seq'::regclass);
 <   ALTER TABLE public.motorcycle ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    215    216    216            A           2604    16493    reservation id    DEFAULT     p   ALTER TABLE ONLY public.reservation ALTER COLUMN id SET DEFAULT nextval('public.reservation_id_seq'::regclass);
 =   ALTER TABLE public.reservation ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    219    220    220            @           2604    16433    reservation_plan id    DEFAULT     z   ALTER TABLE ONLY public.reservation_plan ALTER COLUMN id SET DEFAULT nextval('public.reservation_plan_id_seq'::regclass);
 B   ALTER TABLE public.reservation_plan ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    218    217    218            D           2604    16523    userbase id    DEFAULT     j   ALTER TABLE ONLY public.userbase ALTER COLUMN id SET DEFAULT nextval('public.userbase_id_seq'::regclass);
 :   ALTER TABLE public.userbase ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    221    222    222            �          0    16561 	   deliverer 
   TABLE DATA           �   COPY public.deliverer (id_del, first_name, last_name, date_of_birth, drivers_license, license_photo_url, license_type, created_at) FROM stdin;
    public          postgres    false    224   @       �          0    16591    delivery 
   TABLE DATA           _   COPY public.delivery (id, created_at, deliverer_id, current_status, delivery_cost) FROM stdin;
    public          postgres    false    226   \@       �          0    16421 
   motorcycle 
   TABLE DATA           R   COPY public.motorcycle (id, model, year, license_plate, times_rented) FROM stdin;
    public          postgres    false    216   y@       �          0    16490    reservation 
   TABLE DATA           �   COPY public.reservation (id, created_at, start_date, estimated_end_date, end_date, motorcycle, reservation_plan_id, status, deliverer_id) FROM stdin;
    public          postgres    false    220   �@       �          0    16430    reservation_plan 
   TABLE DATA           M   COPY public.reservation_plan (id, name, rental_days, daily_cost) FROM stdin;
    public          postgres    false    218   
A       �          0    16520    userbase 
   TABLE DATA           x   COPY public.userbase (id, username, password, deliverer_id, email, created_at, active_access, access_level) FROM stdin;
    public          postgres    false    222   VA                  0    0    deliverer_id_del_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.deliverer_id_del_seq', 14, true);
          public          postgres    false    223                       0    0    delivery_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.delivery_id_seq', 1, false);
          public          postgres    false    225                       0    0    motorcycle_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.motorcycle_id_seq', 2, true);
          public          postgres    false    215                       0    0    reservation_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.reservation_id_seq', 2, true);
          public          postgres    false    219            	           0    0    reservation_plan_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.reservation_plan_id_seq', 1, true);
          public          postgres    false    217            
           0    0    userbase_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.userbase_id_seq', 3, true);
          public          postgres    false    221            V           2606    16569 '   deliverer deliverer_drivers_license_key 
   CONSTRAINT     m   ALTER TABLE ONLY public.deliverer
    ADD CONSTRAINT deliverer_drivers_license_key UNIQUE (drivers_license);
 Q   ALTER TABLE ONLY public.deliverer DROP CONSTRAINT deliverer_drivers_license_key;
       public            postgres    false    224            X           2606    16567    deliverer deliverer_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.deliverer
    ADD CONSTRAINT deliverer_pkey PRIMARY KEY (id_del);
 B   ALTER TABLE ONLY public.deliverer DROP CONSTRAINT deliverer_pkey;
       public            postgres    false    224            Z           2606    16600    delivery delivery_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.delivery
    ADD CONSTRAINT delivery_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.delivery DROP CONSTRAINT delivery_pkey;
       public            postgres    false    226            L           2606    16428 '   motorcycle motorcycle_license_plate_key 
   CONSTRAINT     k   ALTER TABLE ONLY public.motorcycle
    ADD CONSTRAINT motorcycle_license_plate_key UNIQUE (license_plate);
 Q   ALTER TABLE ONLY public.motorcycle DROP CONSTRAINT motorcycle_license_plate_key;
       public            postgres    false    216            N           2606    16426    motorcycle motorcycle_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.motorcycle
    ADD CONSTRAINT motorcycle_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.motorcycle DROP CONSTRAINT motorcycle_pkey;
       public            postgres    false    216            R           2606    16496    reservation reservation_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.reservation DROP CONSTRAINT reservation_pkey;
       public            postgres    false    220            P           2606    16437 &   reservation_plan reservation_plan_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.reservation_plan
    ADD CONSTRAINT reservation_plan_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.reservation_plan DROP CONSTRAINT reservation_plan_pkey;
       public            postgres    false    218            T           2606    16529    userbase userbase_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.userbase
    ADD CONSTRAINT userbase_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.userbase DROP CONSTRAINT userbase_pkey;
       public            postgres    false    222            ]           2606    16601 #   delivery delivery_deliverer_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.delivery
    ADD CONSTRAINT delivery_deliverer_id_fkey FOREIGN KEY (deliverer_id) REFERENCES public.deliverer(id_del);
 M   ALTER TABLE ONLY public.delivery DROP CONSTRAINT delivery_deliverer_id_fkey;
       public          postgres    false    226    224    4696            [           2606    16497 '   reservation reservation_motorcycle_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_motorcycle_fkey FOREIGN KEY (motorcycle) REFERENCES public.motorcycle(id);
 Q   ALTER TABLE ONLY public.reservation DROP CONSTRAINT reservation_motorcycle_fkey;
       public          postgres    false    220    4686    216            \           2606    16502 0   reservation reservation_reservation_plan_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_reservation_plan_id_fkey FOREIGN KEY (reservation_plan_id) REFERENCES public.reservation_plan(id);
 Z   ALTER TABLE ONLY public.reservation DROP CONSTRAINT reservation_reservation_plan_id_fkey;
       public          postgres    false    220    218    4688            �   7   x^34�,I-.I��FF&�ƺ�F(�	#+c+C=SsScC�=... ��      �      x^����� � �      �   8   x^3����/��4����t��5461���2��ͅ�r�y8ꆆ��$b���� {��      �   9   x^3�4202�50�54V00�22�21�364�0�@��3�8c�8�ЀӐ+F��� ��      �   <   x^3�4WH�L,VH��U(�UH.-.�WHIUR16�10P(�/)�4�46�30������ �Y�      �   8   x^3�,I-.I���P����D��X��H��������D�����Є3ƏӐ+F��� �#�     