create sequence mp_conversation_id_seq;
CREATE TABLE public.mp_conversation
(
    id bigint NOT NULL DEFAULT nextval('mp_conversation_id_seq'::regclass),
    message text COLLATE pg_catalog."default" NOT NULL,
    "from" uuid NOT NULL,
    "to" uuid NOT NULL,
    created_at timestamp without time zone NOT NULL,
    CONSTRAINT mp_conversation_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.mp_conversation
    OWNER to postgres;

create sequence mp_credit_id_seq;
CREATE TABLE public.mp_credit
(
    id bigint NOT NULL DEFAULT nextval('mp_credit_id_seq'::regclass),
    profile_id uuid NOT NULL,
    amount money NOT NULL,
    created_at timestamp without time zone NOT NULL,
    created_by text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT mp_credit_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.mp_credit
    OWNER to postgres;

create sequence app_nav_id_seq;
CREATE TABLE public.app_nav
(
    id integer NOT NULL DEFAULT nextval('app_nav_id_seq'::regclass),
    name text COLLATE pg_catalog."default" NOT NULL,
    controller text COLLATE pg_catalog."default" NOT NULL,
    action text COLLATE pg_catalog."default" NOT NULL,
    icon text COLLATE pg_catalog."default",
    "order" integer NOT NULL,
    active integer NOT NULL,
    roles text COLLATE pg_catalog."default",
    CONSTRAINT app_nav_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.app_nav
    OWNER to postgres;
	
INSERT INTO public.app_nav(
	id, name, controller, action, icon, "order", active, roles)
	VALUES (1,'Dashboard', 'Home', 'Index', 'pe-7s-display1', 1, 1, 'super_admin,admin');

INSERT INTO public.app_nav(
	id, name, controller, action, icon, "order", active, roles)
	VALUES (2,'My Profile', 'Profile', 'MyProfile', 'pe-7s-id', 2, 1, 'client,clinician');
	
INSERT INTO public.app_nav(
	id, name, controller, action, icon, "order", active, roles)
	VALUES (3,'Applicants', 'Applicant', 'Index', 'fa fa-users', 3, 1, 'super_admin,admin');


----mp_applicant table

ALTER TABLE mp_applicant 
ADD CONSTRAINT fk_applicant_application_status FOREIGN KEY (status) REFERENCES mp_lookup (id);

ALTER TABLE mp_applicant 
ADD CONSTRAINT fk_applicant_marital_status FOREIGN KEY (marital_status) REFERENCES mp_lookup (id);

ALTER TABLE mp_applicant 
ADD CONSTRAINT fk_applicant_mp_country FOREIGN KEY (country) REFERENCES mp_countries (id);

ALTER TABLE mp_applicant 
ADD CONSTRAINT fk_applicant_mp_state FOREIGN KEY (state) REFERENCES mp_states (id);

----mp_profile table

ALTER TABLE mp_profile
ADD CONSTRAINT fk_profile_status FOREIGN KEY (status) REFERENCES mp_lookup (id);

ALTER TABLE mp_profile 
ADD CONSTRAINT fk_profile_marital_status FOREIGN KEY (marital_status) REFERENCES mp_lookup (id);

ALTER TABLE mp_profile 
ADD CONSTRAINT fk_profile_mp_country FOREIGN KEY (country) REFERENCES mp_countries (id);

ALTER TABLE mp_profile 
ADD CONSTRAINT fk_profile_mp_state FOREIGN KEY (state) REFERENCES mp_states (id);


--mp_states table

ALTER TABLE mp_states 
ADD CONSTRAINT fk_mp_states_mp_country FOREIGN KEY (country_id) REFERENCES mp_countries (id);

--mp_countries
alter TABLE mp_countries
add column active integer NOT NULL DEFAULT 0;

update mp_countries set active=1 where id=160;