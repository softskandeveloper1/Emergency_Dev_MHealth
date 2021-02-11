
create sequence mp_expertise_id_seq;
CREATE TABLE public.mp_expertise
(
    id integer NOT NULL DEFAULT nextval('mp_expertise_id_seq'::regclass),
    name text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT mp_expertise_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.mp_expertise
    OWNER to postgres;

create sequence mp_profile_expertise_id_seq;
CREATE TABLE public.mp_profile_expertise
(
    id bigint NOT NULL DEFAULT nextval('mp_profile_expertise_id_seq'::regclass),
    profile_id uuid NOT NULL,
    expertise_id integer NOT NULL,
    CONSTRAINT mp_profile_expertise_pkey PRIMARY KEY (id),
    CONSTRAINT fk_mp_profile_expertise_mp_expertise FOREIGN KEY (expertise_id)
        REFERENCES public.mp_expertise (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_mp_profile_expertise_mp_profile FOREIGN KEY (profile_id)
        REFERENCES public.mp_profile (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.mp_profile_expertise
    OWNER to postgres;
	
create sequence mp_applicant_expertise_id_seq;
CREATE TABLE public.mp_applicant_expertise
(
    id bigint NOT NULL DEFAULT nextval('mp_applicant_expertise_id_seq'::regclass),
    applicant_id uuid NOT NULL,
    expertise_id integer NOT NULL,
    CONSTRAINT mp_applicant_expertise_pkey PRIMARY KEY (id),
    CONSTRAINT fk_mp_applicant_expertise_mp_expertise FOREIGN KEY (expertise_id)
        REFERENCES public.mp_expertise (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_mp_applicant_expertise_mp_profile FOREIGN KEY (applicant_id)
        REFERENCES public.mp_profile (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.mp_applicant_expertise
    OWNER to postgres;